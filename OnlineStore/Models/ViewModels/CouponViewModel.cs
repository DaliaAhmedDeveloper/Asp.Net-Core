using OnlineStore.Models.Enums;

namespace OnlineStore.Models.ViewModels;

public class CouponViewModel
{
    public int Id { get; set; }
    public string TermsAndConditionsEn { get; set; } = string.Empty;
    public string TermsAndConditionsAr { get; set; } = string.Empty;
    public string DescriptionEn { get; set; } = string.Empty;
    public string DescriptionAr { get; set; } = string.Empty;
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
    public ICollection<CategoryTranslation> Translations { get; set; } = new List<CategoryTranslation>();
}