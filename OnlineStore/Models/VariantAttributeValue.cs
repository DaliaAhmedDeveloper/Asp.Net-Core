namespace OnlineStore.Models;

public class VariantAttributeValue
{
    
    public int Id { get; set; }

    public int ProductVariantId { get; set; }
    public ProductVariant ProductVariant { get; set; } = null!;

    public int AttributeId { get; set; }
    public ProductAttribute Attribute { get; set; } = null!;

    public int AttributeValueId { get; set; }
    public AttributeValue AttributeValue { get; set; } = null!;
}
