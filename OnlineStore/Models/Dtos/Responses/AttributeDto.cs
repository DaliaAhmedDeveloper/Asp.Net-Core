namespace OnlineStore.Models.Dtos.Responses;

public class AttributeDto
{
    public int Id { get; set; }
    public string? Slug { get; set; }
    public string? Title { get; set; }
    public AttributeValueDto AttributeValue { get; set; } = null!;
    public ICollection<ProductAttributeTranslation> Translations { get; set; } = new List<ProductAttributeTranslation>();
}
