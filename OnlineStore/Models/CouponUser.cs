namespace OnlineStore.Models;
public class CouponUser
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int CouponId { get; set; }
    public Coupon Coupon { get; set; } = null!;
    public DateTime UsedAt { get; set; } = DateTime.UtcNow;
    public int UsageCount { get; set; } = 1;
    
}
