namespace OnlineStore.Models;

public class ProductAttribute : SoftDeleteEntity
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public ICollection<VariantAttributeValue> VariantAttributeValues { get; set; } = new List<VariantAttributeValue>(); // attribute belongs to many variant 
    public ICollection<AttributeValue> Values { get; set; } = new List<AttributeValue>(); // has many values
    public ICollection<ProductAttributeTranslation> Translations { get; set; } = new List<ProductAttributeTranslation>();
}
