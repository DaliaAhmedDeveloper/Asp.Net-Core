namespace OnlineStore.Models;
public class AttributeValueTranslation 
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // e.g., "Red", "Large", "8GB"
    public string LanguageCode { get; set; } = string.Empty;
    public int AttributeValueId { get; set; }
    public AttributeValue AttributeValue { get; set; } = null!; 
}