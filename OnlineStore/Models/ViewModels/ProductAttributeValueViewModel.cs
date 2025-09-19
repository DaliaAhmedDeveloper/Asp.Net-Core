namespace OnlineStore.Models.ViewModels;
public class ProductAttributeValueViewModel
{
    public int Id { get; set; }
    public int AttributeId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    public ICollection<AttributeValueTranslation> Translations { get; set; } = new List<AttributeValueTranslation>();
}