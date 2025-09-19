
using OnlineStore.Models.Enums;

namespace OnlineStore.Models.Dtos.Responses;

public class OrderDetailsDto
{
    public int Id { get; set; }
    public decimal TotalAmountBeforeSale { get; set; }
    public decimal TotalAmountAfterSale { get; set; }
    public decimal SaleDiscountAmount { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string ReferenceNumber { get; set; } = string.Empty;
    public decimal CouponDiscountAmount { get; set; }
    public CouponDto Coupon { get; set; } = null!;
    public int PointsUsed { get; set; }
    public decimal PointsDiscountAmount { get; set; }
    public decimal WalletAmountUsed { get; set; }
    public decimal FinalAmount { get; set; }
    public ShippingAddressDto ShippingAddress { get; set; } = null!;
    public ShippingMethodDto ShippingMethod { get; set; } = null!;
    public PaymentDto Payment { get; set; } = null!;
    public List<ListOrderItemDto> OrderItems { get; set; } = new List<ListOrderItemDto>();
    public OrderTrackingDto OrderTracking { get; set; } = null!;
    public List<ReturnDto> Returns { get; set; } = new List<ReturnDto>();
}