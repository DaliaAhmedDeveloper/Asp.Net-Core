using OnlineStore.Models.Enums;

namespace OnlineStore.Models.Dtos.Responses;
public class OrderDto
{
    public int Id { get; set; }
    public string CouponCode { get; set; } = string.Empty;
    public decimal CouponDiscountAmount { get; set; }
    public ShippingAddressDto ShippingAddress { get; set; } = null!;
    public string ReferenceNumber { get; set; } = string.Empty;
    public string OrderTracking { get; set; } = string.Empty;
    public OrderStatus OrderStatus { get; set; }
    public DateTime  CreatedAt { get; set; }
    public PaymentMethod PaymentMethod { get; set; } 
    public decimal CashOnDeliveryFee { get; set; }
    public decimal PointsUsed { get; set; }
    public decimal PointsDiscountAmount { get; set; }
    public decimal WalletAmountUsed { get; set; }
    public ShippingMethodDto ShippingMethod { get; set; } = null!;
    public decimal TotalAmountBeforeSale { get; set; }
    public decimal TotalAmountAfterSale { get; set; }
    public decimal SaleDiscountAmount { get; set; } 
    public decimal FinalAmount { get; set; }  
    public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
}
public class OrderItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int ProductVariantId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string AttributeName { get; set; } = string.Empty;
    public string ValueName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
