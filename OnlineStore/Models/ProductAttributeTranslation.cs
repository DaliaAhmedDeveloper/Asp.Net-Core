namespace OnlineStore.Models;

public class ProductAttributeTranslation
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // e.g., "Color", "Size", "RAM"
    public string LanguageCode { get; set; } = string.Empty;

    public int ProductAttributeId { get; set; }

    public ProductAttribute Attribute { get; set; } = null!;
}
