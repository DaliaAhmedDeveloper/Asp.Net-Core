using OnlineStore.Models.Enums;

namespace OnlineStore.Models.ViewModels;

public class ReviewViewModel
{
    public int? Id { get; set; }
    public decimal? TotalAmountBeforeSale { get; set; } // before sale on prices
    public decimal? TotalAmountAfterSale { get; set; } // before sale on prices
    public decimal? SaleDiscountAmount { get; set; }
    public ReturnStatus ReturnStatus { get; set; } = ReturnStatus.Pending;
    public PaymentMethod? PaymentMethod { get; set; }
    public string? ReferenceNumber { get; set; } = string.Empty;
    public string? UserName { get; set; } = string.Empty;
    public string? Coupon { get; set; } = string.Empty;
    public decimal CouponDiscountAmount{ get; set; }
    public decimal PointsDiscountAmount{ get; set; }
    public decimal ShippingMethodCost{ get; set; }
    public decimal CashFees { get; set; }
    public int? PointsUsed { get; set; }
    public decimal? WalletAmountUsed { get; set; }
    public decimal? FinalAmount { get; set; }
    public string? ShippingAddress { get; set; } = string.Empty;
    public string? ShippingMethod { get; set; } = string.Empty;
    public string? Payment { get; set; } = string.Empty;
    public ICollection<ReturnItem>? ReturnItems { get; set; } = new List<ReturnItem>();
}