
using OnlineStore.Models.Enums;

namespace OnlineStore.Models;

public class Order : BaseEntity
{
    public int Id { get; set; }
    public decimal TotalAmountBeforeSale { get; set; } // before sale on prices
    public decimal TotalAmountAfterSale { get; set; } // before sale on prices
    public decimal SaleDiscountAmount { get; set; }
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    public PaymentMethod PaymentMethod { get; set; }
    public string ReferenceNumber { get; set; } = Guid.NewGuid().ToString("N");
    public int? UserId { get; set; }
    public string UserEmail { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public decimal CouponDiscountAmount { get; set; }
    public string Coupon { get; set; } = string.Empty;
    public int PointsUsed { get; set; }
    public decimal PointsDiscountAmount { get; set; }
    public decimal WalletAmountUsed { get; set; }
    public decimal FinalAmount { get; set; }
    // shipping address snapshot
    public string ShAddressFullName { get; set; } = string.Empty;
    public string ShAddressCity { get; set; } = string.Empty;
    public string ShAddressCountry { get; set; } = string.Empty;
    public string ShAddressStreet { get; set; } = string.Empty;
    public string ShAddressZipCode { get; set; } = string.Empty;

    // shipping method relation for analysis
    public int? ShippingMethodId { get; set; }
    // Shipping method snapshot
    public string ShippingMethod { get; set; } = "{}";
    public decimal ShippingMethodCost { get; set; }
    public string ShippingMethodDelieveryDate { get; set; } = string.Empty;
    public Payment Payment { get; set; } = null!; 
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public OrderTracking OrderTracking { get; set; } = null!;
    public ICollection<Return> Returns { get; set; } = new List<Return>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<SupportTicket> SupportTickets { set; get; } = new List<SupportTicket>();

}
