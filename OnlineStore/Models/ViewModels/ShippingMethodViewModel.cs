namespace OnlineStore.Models.ViewModels;

public class ShippingMethodViewModel
{
    public int Id { get; set; }
    public decimal Cost { get; set; }
    public string DeliveryTime { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    
    public ICollection<CategoryTranslation> Translations { get; set; } = new List<CategoryTranslation>();
}