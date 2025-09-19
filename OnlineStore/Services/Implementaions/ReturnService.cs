namespace OnlineStore.Services;

using Microsoft.Extensions.Localization;
using OnlineStore.Helpers;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.ViewModels;
using OnlineStore.Repositories;
using OnlineStore.Services.BackgroundServices;
using System.Threading.Tasks;

public class ReturnService : IReturnService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBackgroundTaskQueue _taskQueue;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IStringLocalizer<ReturnService> _localizer;
    public ReturnService(
        IUnitOfWork unitOfWork,
        IBackgroundTaskQueue taskQueue,
        IServiceScopeFactory scopeFactory,
        IStringLocalizer<ReturnService> localizer
    )
    {
        _unitOfWork = unitOfWork;
        _taskQueue = taskQueue;
        _scopeFactory = scopeFactory;
        _localizer = localizer;
    }

    // return order items
    public async Task RetrunOrderItems(ReturnRequestDto dto)
    {
        // create return request ,, add return add return items ,, return tracking ,, send to shipping company to pikup items
        int orderId = dto.OrderId ?? 0;
        var order = await _unitOfWork.Order.GetByIdAsync(orderId);
        if (order == null)
            throw new NotFoundException(_localizer["OrderNotFound"]);

        Return returnModel = new Return
        {
            OrderId = orderId,
            UserId = order.UserId,
        };

        decimal totalAmount = 0;
        foreach (var item in dto.Items)
        {
            int orderItemId = item.OrderItemId ?? 0;
            var _item = await _unitOfWork.Order.GetOrderItemByIdAsync(orderItemId);
            if (_item == null)
                throw new NotFoundException(_localizer["OrderItemNotFound"]);

            if (_item.Quantity < item.Quantity)
                throw new ResponseErrorException(_localizer["QuantityMismatch"]);

            totalAmount += _item.UnitPrice * item.Quantity;
            returnModel.ReturnItems.Add(new ReturnItem
            {
                OrderItemId = orderItemId,
                Quantity = item.Quantity,
                Reason = item.Reason,
                UnitPrice = _item.UnitPrice
            });
        }
        returnModel.TotalAmount = totalAmount;

        // send emails
        _taskQueue.Enqueue(async token =>
        {
            //scope 
            using var scope = _scopeFactory.CreateAsyncScope();
            var _returnHelper = scope.ServiceProvider.GetRequiredService<ReturnHelper>();
            await _returnHelper.SendOrderEmails(order.User, returnModel);
        });

        // send notifications
        _taskQueue.Enqueue(async token =>
        {
            //scope 
            using var scope = _scopeFactory.CreateAsyncScope();
            var _returnHelper = scope.ServiceProvider.GetRequiredService<ReturnHelper>();
            await _returnHelper.SendOrderNotifications(order.User, returnModel);
        });
        // send to shipping company
        await _unitOfWork.Return.AddAsync(returnModel);

    }

    // web

    // after return success refund money to wallet ,, return back points and wallet amont if used
    public async Task RetrunSuccess(int orderId, List<OrderItemDto> orderItems)
    {
        // change 
    }

    // return tracking
    public async Task RetrunTracking(int orderId, List<OrderItemDto> orderItems)
    {

    }

    // get all with pagination
    public async Task<PagedResult<Return>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _unitOfWork.Return.CountAllAsync();
        var Returns = await _unitOfWork.Return.GetAllWithPaginationAsync(searchTxt, pageNumber, pageSize);
        var model = new PagedResult<Return>
        {
            Items = Returns,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }
public async Task<Return?> GetForWeb(int id)
    {
        return await _unitOfWork.Return.GetForWebWithRelationsAsync(id);
    }

    // // update Return
    // public async Task<Return> UpdateForWeb(ReturnViewModel model, Return Return)
    // {
    //     await _unitOfWork.Return.UpdateAsync(Return, model.ReturnStatus);
    //     // email and notification
    //     return Return;
    // }

    // delete Return
    public async Task<bool> DeleteForWeb(int id)
    {
        var Return = await _unitOfWork.Return.GetByIdAsync(id);
        return await _unitOfWork.Return.DeleteAsync(id);
    }
}
