namespace OnlineStore.Models.Dtos.Responses;

public class ProductWithTranslatvvionsDto
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public decimal SalePrice { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public ICollection<ProductTranslation> Translations { get; set; } = new List<ProductTranslation>();
    public ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}