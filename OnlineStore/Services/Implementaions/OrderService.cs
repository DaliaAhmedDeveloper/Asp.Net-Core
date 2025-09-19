namespace OnlineStore.Services;

using System.Linq;
using System.Transactions;
using Microsoft.Extensions.Localization;
using OnlineStore.Helpers;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.Enums;
using OnlineStore.Models.ViewModels;
using OnlineStore.Repositories;
using OnlineStore.Services.BackgroundServices;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICartService _cart;
    private readonly IBackgroundTaskQueue _taskQueue;
    private readonly OrderHelper _OrderHelper;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IStringLocalizer<OrderService> _localizer;

    public OrderService(
        IUnitOfWork unitOfWork,
        IBackgroundTaskQueue taskQueue,
        OrderHelper OrderHelper,
        ICartService cart,
        IServiceScopeFactory scopeFactory,
        IStringLocalizer<OrderService> localizer
    )
    {
        _unitOfWork = unitOfWork;
        _taskQueue = taskQueue;
        _OrderHelper = OrderHelper;
        _cart = cart;
        _scopeFactory = scopeFactory;
        _localizer = localizer;
    }
    public async Task<IEnumerable<Order>> List()
    {
        return await _unitOfWork.Order.GetAllAsync();
    }
    public async Task<OrderDetailsDto?> Find(int id)
    {
        var order = await _unitOfWork.Order.GetAsync(id);
        if (order == null)
            throw new NotFoundException(_localizer["OrderNotFound"]);

        return order;
    }
    /*
    Place User Order 
    */
    public async Task<OrderDto> Create(CreateOrderDto dto, int userId)
    {
        var order = new Order();
        var orderDto = new OrderDto();
        // Transaction .. to roll back the database changes when any error accour 
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            // chaeck user cart
            var cart = await _cart.CheckUserCart(userId);
            // Check The Stock , items price calculations
            var (beforeDiscount, afterSale) = await _OrderHelper.CheckStock(cart);

            // calculate final price
            var userWallet = await _unitOfWork.Wallet.GetByUserIdAsync(userId);
            decimal finalPrice = await _OrderHelper.FinalPriceCalculations(userId, dto, afterSale, userWallet);

            // total items count to devide wallet and points 
            var totalItemsCount = cart.Items.Sum(i => i.Quantity);
            // payment stripe
            if (dto.PaymentMethod == PaymentMethod.Card)
            {
                // will do later
            }
            var shippingAddress = await _unitOfWork.Address.GetByIdAsync(dto.ShippingAddressId);
            if (shippingAddress == null)
                throw new NotFoundException(_localizer["AddressNotFound"]);

            var shippingMethod = await _unitOfWork.ShippingMethod.GetByIdAsync(dto.ShippingMethodId);
            if (shippingMethod == null)
                throw new NotFoundException(_localizer["ShippingMethodNotFound"]);
            // Order Mapping
            finalPrice += shippingMethod.Cost;
            order = _OrderHelper.OrderMapping(
                userId,
                cart,
                dto,
                beforeDiscount,
                afterSale,
                finalPrice,
                totalItemsCount
                );
            // add order , with ReferenceNumber , Concurrency check
            await _unitOfWork.Order.AddAsync(order);
            orderDto = await _OrderHelper.OrderResponse(order, shippingMethod, shippingAddress);
            // update cart stock wallet and User points (Add it to Queue for performence )
            await _OrderHelper.UpdateCartStockPointsWallet(userId, dto, userWallet);
            scope.Complete();
        }
        // send notifications in background Queue
        _taskQueue.Enqueue(async token =>
        {
            using var scope = _scopeFactory.CreateAsyncScope();
            var _OrderHelper = scope.ServiceProvider.GetRequiredService<OrderHelper>();
            await _OrderHelper.SendOrderNotifications(userId, order);
        });
        // send emails in background Queue
        _taskQueue.Enqueue(async token =>
       {
           using var scope = _scopeFactory.CreateAsyncScope();
           var _OrderHelper = scope.ServiceProvider.GetRequiredService<OrderHelper>();
           await _OrderHelper.SendOrderEmails(userId, order);
       });
        return orderDto;
    }

    // cancel order
    public async Task<bool> CancelOrder(int orderId)
    {
        //Check Order Exists
        var order = await _unitOfWork.Order.GetWithItemsByIdAsync(orderId);
        if (order == null)
            throw new NotFoundException(_localizer["OrderNotFound"]);
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            // check if order status is pernding or processing
            if (order.OrderStatus != OrderStatus.Pending && order.OrderStatus != OrderStatus.Processing && order.OrderStatus != OrderStatus.Approved)
                throw new ResponseErrorException(_localizer["CannotCancelOrder"]);

            // Updata order status to cancel
            await _unitOfWork.Order.UpdateAsync(order, OrderStatus.Cancelled);
            await _OrderHelper.OrderReverse(order);
            scope.Complete();
        }

        // send notifications for admins and user 
        _taskQueue.Enqueue(async token =>
        {
            using var scope = _scopeFactory.CreateAsyncScope();
            var _OrderHelper = scope.ServiceProvider.GetRequiredService<OrderHelper>();
            await _OrderHelper.SendOrderStatusNotifications(order);
        });
        // send email for admins and user 
        _taskQueue.Enqueue(async token =>
        {
            using var scope = _scopeFactory.CreateAsyncScope();
            var _OrderHelper = scope.ServiceProvider.GetRequiredService<OrderHelper>();
            await _OrderHelper.SendOrderStatusEmails(order);
        });
        return true;
    }
    // track order
    public async Task<OrderTrackingDto> TrackOrder(string orderNumber)
    {
        // get order by transaction id
        var order = await _unitOfWork.Order.GetByReferenceNumberAsync(orderNumber);
        if (order == null)
            throw new NotFoundException(_localizer["OrderNotFound"]);

        string trackingNumber = string.Empty;
        if (order.OrderStatus != OrderStatus.Pending && order.OrderStatus != OrderStatus.Approved && order.OrderStatus != OrderStatus.Processing)
        {
            trackingNumber = order.OrderTracking.TrackingNumber;
        }
        var dto = new OrderTrackingDto
        {
            OrderNumber = order.ReferenceNumber,
            Status = order.OrderStatus,
            TrackingNumber = trackingNumber,
            TrackingUrl = order.OrderTracking.TrackingUrl,
            DriverName = order.OrderTracking.DriverName,
            DriverPhone = order.OrderTracking.DriverPhone,
        };
        return dto;
    }

    // update status 
    public async Task<Order> UpdateStatus(Order order, EditOrderViewModel model)
    {
        OrderStatus status = model.OrderStatus;
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            if (status == OrderStatus.Cancelled || status == OrderStatus.Rejected)
            {
                await _OrderHelper.OrderReverse(order);

            }
            else if (status == OrderStatus.Shipped)
            {
                OrderTracking tracking = new OrderTracking
                {
                    OrderId = order.Id,
                    TrackingUrl = "Waiting for shipping company api integration",
                    TrackingNumber = "Waiting for shipping company api integration",

                };
                await _unitOfWork.OrderTracking.UpdateAsync(tracking);
            }
            else if (status == OrderStatus.OutForDeleviery)
            {
                // update trackshipping information
                OrderTracking tracking = new OrderTracking
                {
                    OrderId = order.Id,
                    DriverName = "Waiting for shipping company api integration",
                    DriverPhone = "Waiting for shipping company api integration",
                };
                await _unitOfWork.OrderTracking.UpdateAsync(tracking);

            }
            else if (status == OrderStatus.Delivered)
            {
                // allow user to add reviews ,, update the stock
                foreach (var item in order.OrderItems)
                {
                    int variantId = item.ProductVariantId;
                    var stock = await _unitOfWork.Stock.GetByVariantIdAsync(variantId);
                    stock.ReservedQuantity -= item.Quantity;
                    stock.TotalQuantity -= item.Quantity;
                    await _unitOfWork.Stock.UpdateAsync(stock);
                }
            }
            scope.Complete();
        }
        //notifications to user with new order status
        _taskQueue.Enqueue(async token =>
        {
            using var scope = _scopeFactory.CreateAsyncScope();
            var _OrderHelper = scope.ServiceProvider.GetRequiredService<OrderHelper>();
            await _OrderHelper.SendOrderStatusNotifications(order);
        });
        // emails to user with new order status
        _taskQueue.Enqueue(async token =>
        {
            using var scope = _scopeFactory.CreateAsyncScope();
            var _OrderHelper = scope.ServiceProvider.GetRequiredService<OrderHelper>();
            await _OrderHelper.SendOrderStatusEmails(order);
        });
        return order;
    }
    // delete
    public async Task<bool> Delete(int id)
    {
        bool status = await _unitOfWork.Order.DeleteAsync(id);
        if (!status)
            throw new NotFoundException(_localizer["OrderNotFound"]);

        return status;
    }
    // list all by user 
    public async Task<IEnumerable<OrderListDto>> ListAllByUser(int userId)
    {
        return await _unitOfWork.Order.GetAllByUserAsync(userId);
    }

    // orders inside current month
    public async Task<IEnumerable<Order>> ListOrdersCurrentMonth()
    {
        return await _unitOfWork.Order.GetAllCurrentMonthAsync();
    }

    /*=========== WEB ========================*/
    // get
    public async Task<IEnumerable<Order>> GetAllForWeb()
    {
        return await _unitOfWork.Order.GetAllAsync();
    }
    // get all with pagination
    public async Task<PagedResult<Order>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _unitOfWork.Order.CountAllAsync();
        var orders = await _unitOfWork.Order.GetAllWithPaginationAsync(searchTxt, pageNumber, pageSize);
        var model = new PagedResult<Order>
        {
            Items = orders,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }
    // get details
    public async Task<Order?> GetForWeb(int id)
    {
        return await _unitOfWork.Order.GetForWebWithRelationsAsync(id);
    }

    // update Order
    public async Task<Order> UpdateForWeb(OrderViewModel model, Order order)
    {
        await _unitOfWork.Order.UpdateAsync(order, model.OrderStatus);
        // email and notification
        return order;
    }

    // delete Order
    public async Task<bool> DeleteForWeb(int id)
    {
        //var order = await _unitOfWork.Order.GetByIdAsync(id);
        return await _unitOfWork.Order.DeleteAsync(id);
    }

    // latest oreders

}