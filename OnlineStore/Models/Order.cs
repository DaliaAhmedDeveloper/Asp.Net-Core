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
    public int UserId { get; set; }
    public User User { get; set; } = null!;  // belongs to one user
    public int? CouponId { get; set; }
    public decimal CouponDiscountAmount { get; set; }
    public Coupon? Coupon { get; set; }
    public int PointsUsed { get; set; }
    public decimal PointsDiscountAmount { get; set; }
    public decimal WalletAmountUsed { get; set; }
    public decimal FinalAmount { get; set; }
    public int ShippingAddressId { get; set; }
    public Address ShippingAddress { get; set; } = null!;
    public int ShippingMethodId { get; set; }
    public ShippingMethod ShippingMethod { get; set; } = null!;
    public Payment Payment { get; set; } = null!; // belongs to one payment ( its one to one relation )
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public OrderTracking OrderTracking { get; set; } = null!;
    public ICollection<Return> Returns { get; set; } = new List<Return>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<SupportTicket> SupportTickets { set; get; } = new List<SupportTicket>();

}
