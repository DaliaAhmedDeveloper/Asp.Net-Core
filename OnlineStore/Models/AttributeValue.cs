namespace OnlineStore.Models;

public class AttributeValue
{
    public int Id { get; set; }
    public int AttributeId { get; set; }
    public string? Code { get; set; }
    public ProductAttribute Attribute { get; set; } = null!; // ProductAttributeValue belongs to one ProductAttribute
    public ICollection<VariantAttributeValue> VariantAttributeValues { get; set; } = new List<VariantAttributeValue>(); // attribute belongs to many variant 
    public ICollection<AttributeValueTranslation> Translations  { get; set; } = new List<AttributeValueTranslation>();
}