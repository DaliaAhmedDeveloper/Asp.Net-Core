namespace OnlineStore.Models;

using OnlineStore.Models.Enums;
public class CouponTranslation
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? TermsAndConditions { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
    public int CouponId { get; set; }
    public Coupon Coupon { get; set; } = null!;
}
