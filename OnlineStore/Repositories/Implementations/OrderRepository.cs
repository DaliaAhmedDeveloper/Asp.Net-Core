namespace OnlineStore.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.Enums;
using OnlineStore.Services;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    private readonly ILanguageService _lang;
    public OrderRepository(AppDbContext context, ILanguageService lang) : base(context)
    {
        _lang = lang;
    }
    public async Task<bool> UpdateItemAsync(OrderItem entity)
    {
        _context.OrderItems.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }
    // get order details
    public async Task<OrderDetailsDto?> GetAsync(int id)
    {
        var lang = _lang.GetCurrentLanguage();
        return await _context.Orders.Select(o => new OrderDetailsDto
        {
            Id = o.Id,
            TotalAmountBeforeSale = o.TotalAmountBeforeSale,
            TotalAmountAfterSale = o.TotalAmountAfterSale,
            SaleDiscountAmount = o.SaleDiscountAmount,
            OrderStatus = o.OrderStatus,
            PaymentMethod = o.PaymentMethod,
            ReferenceNumber = o.ReferenceNumber,
            CouponDiscountAmount = o.CouponDiscountAmount,
            Coupon = o.Coupon == null ? new CouponDto() : new CouponDto
            {
                Id = o.Coupon.Id,
                Code = o.Coupon.Code,
            },
            PointsUsed = o.PointsUsed,
            PointsDiscountAmount = o.PointsDiscountAmount,
            WalletAmountUsed = o.WalletAmountUsed,
            FinalAmount = o.FinalAmount,
            ShippingAddress = o.ShippingAddress == null ? new ShippingAddressDto() : new ShippingAddressDto
            {
                FullName = o.ShippingAddress.FullName,
                City = o.ShippingAddress.City,
                Country = o.ShippingAddress.Country,
                Street = o.ShippingAddress.Street,
                ZipCode = o.ShippingAddress.ZipCode,
                UserId = o.ShippingAddress.UserId
            },

            ShippingMethod = o.ShippingMethod == null ? new ShippingMethodDto() : new ShippingMethodDto
            {
                Id = o.ShippingMethod.Id,
                Name = o.ShippingMethod.Name,
                Cost = o.ShippingMethod.Cost,
                DeliveryTime = o.ShippingMethod.DeliveryTime
            },

            Payment = o.Payment == null ? new PaymentDto() : new PaymentDto
            {
                Id = o.Payment.Id,
                Amount = o.Payment.Amount,
                PaymentDate = o.Payment.PaymentDate
            },
            OrderItems = o.OrderItems.Select(oi => new ListOrderItemDto
            {
                Id = oi.Id,
                ProductName = oi.Product.Translations
                 .Where(tr => tr.LanguageCode == lang)
                 .Select(tr => tr.Name)
                 .FirstOrDefault() ?? "",
                ImageUrl = oi.ProductVariant.ImageUrl ?? oi.Product.ImageUrl
            }).ToList(),
            OrderTracking = o.OrderTracking == null ? new OrderTrackingDto() : new OrderTrackingDto
            {
                OrderNumber = o.ReferenceNumber,
                Status = o.OrderTracking.Status,
                TrackingNumber = o.OrderTracking.TrackingNumber,
                TrackingUrl = o.OrderTracking.TrackingUrl,
                DriverName = o.OrderTracking.DriverName,
                DriverPhone = o.OrderTracking.DriverPhone,
            },
            Returns = o.Returns.Select(r => new ReturnDto
            {
                Id = r.Id,
                OrderId = r.OrderId,
                UserId = r.UserId,
                TotalAmount = r.TotalAmount,
                ReferenceNumber = r.ReferenceNumber,
                Reason = r.Reason,
                Status = r.Status.ToString(),
                ReturnDate = r.ReturnDate,
                RefundType = r.RefundType.ToString(),

                ReturnItems = r.ReturnItems.Select(ri => new ReturnItemDto
                {
                    Id = ri.Id,
                    OrderItemId = ri.OrderItemId,
                    ProductName = ri.OrderItem.Product.Translations.Where(tr => tr.LanguageCode == lang)
                    .Select(tr => tr.Name).FirstOrDefault() ?? "",
                    Reason = ri.Reason,
                    Quantity = ri.Quantity,
                    UnitPrice = ri.UnitPrice,
                    Subtotal = ri.Subtotal,

                    Attachments = ri.Attachments.Select(a => new ReturnAttachmentDto
                    {
                        Id = a.Id,
                        FileUrl = a.FileName
                    }).ToList()
                }).ToList(),

                ReturnTracking = r.ReturnTracking == null ? null : new ReturnTrackingDto
                {
                    Id = r.ReturnTracking.Id,
                    Status = r.ReturnTracking.Status.ToString(),
                    TrackingNumber = r.ReturnTracking.TrackingNumber,
                    TrackingUrl = r.ReturnTracking.TrackingUrl,
                    DriverName = "",
                    DriverPhone = "",
                }
            }).ToList()
        }).FirstOrDefaultAsync(o => o.Id == id);
    }

    // get all by user
    public async Task<IEnumerable<OrderListDto>> GetAllByUserAsync(int userId)
    {
        var lang = _lang.GetCurrentLanguage();
        // using projection 
        // return await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
        return await _context.Orders.Where(o => o.UserId == userId).Select(o => new OrderListDto
        {
            Id = o.Id,
            OrderStatus = o.OrderStatus,
            ReferenceNumber = o.ReferenceNumber,
            FinalAmount = o.FinalAmount,
            ItemCount = o.OrderItems.Sum(oi => oi.Quantity),

            Items = o.OrderItems.Select(oi => new ListOrderItemDto
            {
                Id = oi.Id,
                ProductName = oi.Product.Translations
                .Where(tr => tr.LanguageCode == lang)
                .Select(tr => tr.Name)
                .FirstOrDefault() ?? "",
                ImageUrl = oi.ProductVariant.ImageUrl ?? oi.Product.ImageUrl
            }).ToList()
        })
    .ToListAsync();
    }

    // get by transaction id
    public async Task<Order?> GetByReferenceNumberAsync(string ReferenceNumber)
    {
        return await _context.Orders.Include(o => o.OrderTracking).FirstOrDefaultAsync(o => o.ReferenceNumber == ReferenceNumber);
    }
    // get with items
    public async Task<Order?> GetWithItemsByIdAsync(int orderId)
    {
        return await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == orderId);
    }
    // update Order
    public async Task<bool> UpdateAsync(Order order, OrderStatus status)
    {
        order.OrderStatus = status;
        //_context.Orders.Update(order);
        return await _context.SaveChangesAsync() > 0; // returns true if at least one row affected
    }
    // this month
    public async Task<IEnumerable<Order>> GetAllCurrentMonthAsync()
    {
        var now = DateTime.UtcNow;
        var startOfMonth = new DateTime(now.Year, now.Month, 1);
        var endOfMonth = startOfMonth.AddMonths(1).AddTicks(-1);

        return await _context.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.CreatedAt >= startOfMonth && o.CreatedAt <= endOfMonth)
            .ToListAsync();
    }
    // web
    public async Task<Order?> GetForWebWithRelationsAsync(int id)
    {
        return await _context.Orders
        .Where(o => o.Id == id)
        .Include(o => o.User)
        .Include(o => o.Coupon)
        .Include(o => o.Payment)
        .Include(o => o.OrderItems)
        .ThenInclude(oi => oi.Product)
        .Include(o => o.OrderItems)
        .ThenInclude(oi => oi.ProductVariant)
        .Include(o => o.ShippingAddress)
        .Include(o => o.ShippingMethod)
        .FirstOrDefaultAsync();
    }

    // get all with pagination 
    public async Task<IEnumerable<Order>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {
        IQueryable<Order> query = _context.Orders.OrderByDescending(o => o.CreatedAt).Include(o => o.User);
        if (!string.IsNullOrEmpty(searchTxt))
            return await query.Where(o => o.ReferenceNumber.Contains(searchTxt)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    // Get Order Item By Id Async
    public async Task<OrderItem?> GetOrderItemByIdAsync(int id)
    {
        return await _context.OrderItems.FirstOrDefaultAsync(oi => oi.Id == id);
    }

    // total order income
    public async Task<decimal> TotalIncome()
    {
        return await _context.Orders.Where(o => o.OrderStatus == OrderStatus.Delivered).SumAsync(o => o.FinalAmount);
    }

    // total order income current month
    public async Task<decimal> TotalIncomeCurrentMonth()
    {
        var now = DateTime.UtcNow;
        var startOfMonth = new DateTime(now.Year, now.Month, 1);
        var endOfMonth = startOfMonth.AddMonths(1).AddTicks(-1);
        return await _context.Orders.
        Where(o => o.OrderStatus == OrderStatus.Delivered)
        .Where(o => o.CreatedAt >= startOfMonth && o.CreatedAt <= endOfMonth)
        .SumAsync(o => o.FinalAmount);
    }
}
