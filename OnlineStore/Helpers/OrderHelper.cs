namespace OnlineStore.Helpers;

using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.Enums;
using OnlineStore.Notifications;
using OnlineStore.Repositories;
using OnlineStore.Services;
public class OrderHelper
{
    private readonly ICouponService _coupon;
    private readonly IWalletService _wallet;
    private readonly IUserPointService _userPoint;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _email;
    private readonly AppSettingHelper _setting;
    private readonly PushNotificationHelper _push;
    private readonly IStringLocalizer<OrderHelper> _localizer;
    private static decimal CouponDiscountValue { get; set; }
    private static decimal PointsDiscountValue { get; set; }
    public OrderHelper(
        ICouponService coupon,
        IWalletService wallet,
        IUserPointService userPoint,
        IUnitOfWork unitOfWork,
        IEmailService email,
        AppSettingHelper setting,
        PushNotificationHelper push,
        IStringLocalizer<OrderHelper> localizer
    )
    {
        _coupon = coupon;
        _wallet = wallet;
        _userPoint = userPoint;
        _unitOfWork = unitOfWork;
        _email = email;
        _setting = setting;
        _push = push;
        _localizer = localizer;
    }
    /*
    Order Mapping
    */
    public Order OrderMapping(
        int userId,
        CartDto cart,
        CreateOrderDto dto,
        decimal beforeDiscount,
        decimal afterSale,
        decimal finalPrice,
        int totalItemsCount
        )
    {
        var order = new Order
        {
            TotalAmountBeforeSale = beforeDiscount,
            TotalAmountAfterSale = afterSale,
            SaleDiscountAmount = beforeDiscount - afterSale,
            FinalAmount = finalPrice,
            PaymentMethod = dto.PaymentMethod,
            ReferenceNumber = Guid.NewGuid().ToString(),
            UserId = userId,
            CouponId = dto.CouponId,
            CouponDiscountAmount = CouponDiscountValue,
            PointsDiscountAmount = PointsDiscountValue,
            ShippingAddressId = dto.ShippingAddressId,
            PointsUsed = dto.PointsUsed,
            WalletAmountUsed = dto.WalletAmountUsed,
            ShippingMethodId = dto.ShippingMethodId,
            OrderTracking = new OrderTracking(),
            Payment = new Payment
            {
                Amount = finalPrice,
                PaymentDate = DateTime.UtcNow,
                Translations = new List<PaymentTranslation>()
                {
                    new PaymentTranslation
                    {
                        LanguageCode = "en",
                        Method = "Cash Payment",
                        Status = "Pay with cash on delivery"
                    },
                    new PaymentTranslation
                    {
                        LanguageCode = "ar",
                        Method= "الدفع نقداً",
                        Status = "الدفع عند الاستلام"
                    }
                }
            },
            OrderItems = cart.Items.Select(i => new OrderItem
            {
                ProductId = i.Product.Id,
                ProductVariantId = i.ProductVariantId,
                Quantity = i.Quantity,
                UnitPrice = i.Product.SalePrice ?? i.Product.Price,
                Points = (int)Math.Round(dto.PointsUsed / (decimal)totalItemsCount),
                WalletAmount = dto.WalletAmountUsed / totalItemsCount
            }).ToList()
        };
        return order;
    }

    // order response 
    public async Task<OrderDto> OrderResponse(Order order, ShippingMethod shippingMethod, Address shippingAddress)
    {
        var orderDto = new OrderDto
        {
            TotalAmountBeforeSale = order.TotalAmountBeforeSale,
            TotalAmountAfterSale = order.TotalAmountAfterSale,
            SaleDiscountAmount = order.TotalAmountBeforeSale - order.TotalAmountAfterSale,
            FinalAmount = order.FinalAmount,
            PaymentMethod = order.PaymentMethod,
            CashOnDeliveryFee = decimal.TryParse(await _setting.GetValue("cash_on_delivery_fees"), out var fee) ? fee : 0m,
            ReferenceNumber = order.ReferenceNumber,
            CouponCode = order.Coupon?.Code ?? "",
            CouponDiscountAmount = order.CouponDiscountAmount,
            PointsDiscountAmount = order.PointsDiscountAmount,
            ShippingAddress = new ShippingAddressDto
            {
                FullName = shippingAddress.FullName,
                City = shippingAddress.City,
                Country = shippingAddress.Country,
                Street = shippingAddress.Street,
                ZipCode = shippingAddress.ZipCode,
                IsDefault = shippingAddress.IsDefault,
                UserId = shippingAddress.Id,
            },
            PointsUsed = order.PointsUsed,
            WalletAmountUsed = order.WalletAmountUsed,
            ShippingMethod = new ShippingMethodDto
            {
                Name = shippingMethod.Name,
                Cost = shippingMethod.Cost,
                DeliveryTime = shippingMethod.DeliveryTime,
            },
            OrderTracking = order.OrderTracking.TrackingNumber,
            // Items = order.OrderItems.Select(oi => new OrderItemDto
            // {
            //     ProductName = oi.Product.Slug,
            //     Quantity = oi.Quantity,
            //     UnitPrice = oi.UnitPrice,
            //     ImageUrl = oi.ProductVariant.ImageUrl ?? oi.Product.ImageUrl,
            //     AttributeName = oi.ProductVariant.VariantAttributeValues.FirstOrDefault()?.Attribute.Code ?? "",
            //     ValueName = oi.ProductVariant.VariantAttributeValues.FirstOrDefault()?.AttributeValue.Code ?? "",

            // }).ToList()
        };
        return orderDto;
    }
    private async Task<decimal> ApplyPaymentFee(decimal finalPrice, PaymentMethod paymentMethod)
    {
        // applay cash on deleivery
        if (paymentMethod == PaymentMethod.Cash)
        {
            var cashFees = decimal.TryParse(await _setting.GetValue("cash_on_delivery_fees"), out var fee) ? fee : 0m;
            finalPrice += cashFees; // add 20 Dollars
        }
        return finalPrice;
    }
    /* Calculate Final Price */
    public async Task<decimal> FinalPriceCalculations(int userId, CreateOrderDto dto, decimal afterSale, Wallet? userWallet)
    {
        decimal finalPrice = afterSale;

        if (dto.WalletAmountUsed != 0 && (userWallet == null || userWallet.Balance < dto.WalletAmountUsed))
            throw new ResponseErrorException(_localizer["WalletBalanceNotEnough"]);

        // Check Coupon ,apply calculations on final price , return updated final price
        var chaeckCoupon = await _coupon.CheckCoupon(dto.CouponId, userId, afterSale, finalPrice);
        finalPrice = chaeckCoupon.finalPrice;
        CouponDiscountValue = chaeckCoupon.couponDiscountValue;

        // check if wallet has the amount
        if (userWallet != null)
        {
            finalPrice = _wallet.CheckWallet(dto.WalletAmountUsed, finalPrice, userWallet);
        }
        else
        {
            dto.WalletAmountUsed = 0;
        }
        // User Points Check
        var checkPoints = await _userPoint.CheckUserPoints(userId, dto.PointsUsed, finalPrice);
        finalPrice = checkPoints.finalPrice;
        PointsDiscountValue = checkPoints.pointsDiscountValue;
        // add 20 dollars if its cash payment
        finalPrice = await ApplyPaymentFee(finalPrice, dto.PaymentMethod);

        // After applying coupon, wallet, and points, finalPrice may go negative
        if (finalPrice < 0) finalPrice = 0;

        return finalPrice;
    }

    /*
 Check Stock
 */
    public async Task<(decimal TotalBefore, decimal TotalAfter)> CheckStock(CartDto cart)
    {
        decimal TotalPriceBeforeSaleDiscount = 0m;
        decimal TotalPriceAfterSale = 0m;

        // check the stocks 
        foreach (var item in cart.Items)
        {
            var stock = await _unitOfWork.Stock.GetByVariantIdAsync(item.ProductVariantId);

            if (stock == null)
                throw new NotFoundException(string.Format(_localizer["ItemOutOfStock"], item.ProductVariantId));

            // check if the stock is enough 
            if ((stock.AvailableQuantity - stock.ReservedQuantity) < item.Quantity)
                throw new NotFoundException(string.Format(_localizer["NotEnoughStock"], item.ProductVariantId));

            // update the stock 
            stock.ReservedQuantity += item.Quantity;
            await _unitOfWork.Stock.UpdateAsync(stock);

            var variant = await _unitOfWork.ProductVariant.GetByIdAsync(item.ProductVariantId);
            if (variant != null)
            {
                var product = await _unitOfWork.Product.GetByIdAsync(variant.ProductId);
                if (product == null)
                    throw new NotFoundException(_localizer["ProductNotFound"]);

                var price = variant.Price > 0 ? variant.Price : product.Price;
                var salePrice = variant.SalePrice > 0 ? variant.SalePrice : product.SalePrice;

                TotalPriceBeforeSaleDiscount += price * item.Quantity;
                TotalPriceAfterSale += (decimal)((salePrice > 0 ? salePrice : price) * item.Quantity);
            }
        }
        return (TotalPriceBeforeSaleDiscount, TotalPriceAfterSale);
    }
    /*
 update - Empty Cart
 Update The Stock
 Update User Pints
 Update User Wallet
 */
    public async Task UpdateCartStockPointsWallet(int userId, CreateOrderDto dto, Wallet? wallet)
    {
        // empty cart
        await _unitOfWork.CartItem.DeleteAllAsync(userId);
        // update Wallet
        if (wallet != null)
        {
            wallet.Balance -= dto.WalletAmountUsed;
            await _unitOfWork.Wallet.UpdateAsync(wallet);
        }
        // update User Points
        var user = await _unitOfWork.User.GetByIdAsync(userId);
        if (user == null)
            throw new NotFoundException(_localizer["UserNotFound"]);

        if (user.UserAvailablePoints > dto.PointsUsed)
        {
            user.UserAvailablePoints -= dto.PointsUsed;
        }
        await _unitOfWork.User.UpdateAsync(user);
    }
    /*
    Send Order Related Notifications
    */
    public async Task SendOrderNotifications(int userId, Order order)
    {
        var referenceNumber = order.ReferenceNumber;
        var userEmail = order.User.Email;
        // Notifications & email to user
        var UserNotification = PlaceOrderAdminNotification.Build(userId, referenceNumber);
        await _unitOfWork.Notification.AddAsync(UserNotification);

        // Notifications & email to admins
        var admins = await _unitOfWork.User.GetAdminsAsync();
        foreach (var admin in admins)
        {
            var adminNotification = PlaceOrderAdminNotification.Build(admin.Id, referenceNumber);
            await _unitOfWork.Notification.AddAsync(adminNotification);
            // signalR
            var notificationDto = new NotificationDto
            {
                Type = adminNotification.Type,
                Url = adminNotification.Url,
                Title = adminNotification.Translations.Where(tr => tr.LanguageCode == "en").Select(tr => tr.Title).FirstOrDefault() ?? "",
                Message = adminNotification.Translations.Where(tr => tr.LanguageCode == "en").Select(tr => tr.Message).FirstOrDefault() ?? "",
                NotificationRelated = PushNotificationType.NewOrder.ToString()
            };
            // push notification
            await _push.PushToUser(admin.Id, notificationDto);
        }
    }
    // send order email
    public async Task SendOrderEmails(int userId, Order order)
    {
        var userEmail = order.User.Email;
        var templateService = new EmailTemplateService();
        var userEmailBody = await templateService.RenderAsync("User/NewOrder", order);
        await _email.SendEmailAsync(userEmail, "Order Received", userEmailBody);

        await Task.Delay(30000);
        // send admin email
        var adminEmailBody = await templateService.RenderAsync("Admin/NewOrder", order);
        await _email.SendEmailAsync(await _setting.GetValue("admin_email"), "New Order", adminEmailBody);
    }
    //send order status
    public async Task SendOrderStatusNotifications(Order order)
    {
        // Notifications & email to user
        var orderStatus = order.OrderStatus;
        var referenceNumber = order.ReferenceNumber;
        var userId = order.UserId;
        var userEmail = order.User.Email;
        var UserNotification = OrderStatusUserNotification.Build(userId, referenceNumber, orderStatus);

        await _unitOfWork.Notification.AddAsync(UserNotification);
        
        if (orderStatus == OrderStatus.Cancelled)
        {
            // send notification to admins 
            var admins = await _unitOfWork.User.GetAdminsAsync();
            foreach (var admin in admins)
            {
                var adminNotification = OrderStatusAdminNotification.Build(admin.Id, referenceNumber, orderStatus);
                await _unitOfWork.Notification.AddAsync(adminNotification);
                // signalR
                var notificationDto = new NotificationDto
                {
                    Type = adminNotification.Type,
                    Url = adminNotification.Url,
                    Title = adminNotification.Translations.Where(tr => tr.LanguageCode == "en").Select(tr => tr.Title).FirstOrDefault() ?? "",
                    Message = adminNotification.Translations.Where(tr => tr.LanguageCode == "en").Select(tr => tr.Message).FirstOrDefault() ?? "",
                    NotificationRelated = PushNotificationType.OrderStatus.ToString()
                };
                // push notification
                await _push.PushToUser(admin.Id, notificationDto);
            }
        }
    }

     //send order status
    public async Task SendOrderStatusEmails(Order order)
    {
        var orderStatus = order.OrderStatus;
        var referenceNumber = order.ReferenceNumber;
        var userEmail = order.User.Email;
        var templateService = new EmailTemplateService();
        var userEmailBody = await templateService.RenderAsync("User/OrderStatus", order);
        
        await _email.SendEmailAsync(userEmail, $"Order {referenceNumber} {orderStatus}", userEmailBody);
        
        await Task.Delay(30000);

        if (orderStatus == OrderStatus.Cancelled)
        {
            var adminEmailBody = await templateService.RenderAsync("Admin/CancelOrder", order);
            // send email
            await _email.SendEmailAsync(
                await _setting.GetValue("admin_email"), $"Order {referenceNumber} {orderStatus}", adminEmailBody
            );
        }
    }

    // oreder reverse
    public async Task OrderReverse(Order order)
    {

        // return wallet 
        if (order.WalletAmountUsed > 0)
        {
            var wallet = await _unitOfWork.Wallet.GetByUserIdAsync(order.UserId);
            if (wallet == null)
            {
                Wallet newWallet = new Wallet { UserId = order.UserId };
                wallet = await _unitOfWork.Wallet.AddAsync(newWallet);
            }
            wallet.Balance += order.WalletAmountUsed;
            // return money to wallet if its card payment
            if (order.PaymentMethod == PaymentMethod.Card && order.FinalAmount > 0)
            {
                wallet.Transactions.Add(new WalletTransaction
                {
                    Amount = order.FinalAmount,
                    Description = $"Your card payment of {order.FinalAmount} has been refunded to your wallet after order status changed to {order.OrderStatus}."
                });
                wallet.Balance += order.FinalAmount;
            }
            // Refund used wallet amount
            wallet.Transactions.Add(new WalletTransaction
            {
                Amount = order.WalletAmountUsed,
                Description = $"Your previously used wallet amount of {order.WalletAmountUsed} has been refunded to your wallet after  order status changed to {order.OrderStatus}."
            });
            await _unitOfWork.Wallet.UpdateAsync(wallet);
        }

        // check order items ,, and return points and wallet 
        var user = await _unitOfWork.User.GetByIdAsync(order.UserId);
        if (user != null)
        {
            if (order.PointsUsed > 0)
            {
                user.UserAvailablePoints += order.PointsUsed;
                user.Points.Add(new UserPoint
                {
                    Points = order.PointsUsed,
                    Type = PointType.OrderCancellation
                });
                await _unitOfWork.User.UpdateAsync(user);
            }
        }
        // update stock
        foreach (var item in order.OrderItems)
        {
            var stock = await _unitOfWork.Stock.GetByVariantIdAsync(item.ProductVariantId);
            stock.ReservedQuantity -= item.Quantity;
            await _unitOfWork.Stock.UpdateAsync(stock);
        }
    }
}