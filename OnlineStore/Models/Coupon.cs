namespace OnlineStore.Models;

using OnlineStore.Models.Enums;
public class Coupon : SoftDeleteEntity
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public DiscountType DiscountType { get; set; } // 0 for Fixed, 1 for Percentage
    public decimal? DiscountValue { get; set; }
    public int? DiscountPrecentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; } // expiry
    public int MaxUsagePerUser { get; set; }
    public decimal MaxDiscountAmount { get; set; }
    public decimal? MinimumOrderAmount { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsForFirstOrderOnly { get; set; }

    // public ICollection<UserGroup> UserGroups { get; set; }
    public ICollection<CouponTranslation> Translations { get; set; } = new List<CouponTranslation>();
    public ICollection<User> Users { get; set; } = new List<User>(); // user can have list of copouns to choose from
    public ICollection<Order> Orders { get; set; } = new List<Order>(); // has many orders
    public ICollection<CouponUser> CouponUsers { get; set; } = new List<CouponUser>();
}
