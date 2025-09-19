
namespace OnlineStore.Models.Dtos.Responses;
public class ProductVariantDto
{
    public int Id { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
    public decimal? SalePrice { get; set; }
    public int Stock { get; set; }
    public List<VariantAttributeValueDto> VariantAttributes { get; set; } = new();
}
