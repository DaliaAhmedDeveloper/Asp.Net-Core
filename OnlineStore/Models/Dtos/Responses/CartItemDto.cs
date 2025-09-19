namespace OnlineStore.Models.Dtos.Responses;

public class CartItemDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int ProductVariantId { get; set; }
    public ProductSimpleDto Product { get; set; } = null!;
    public ProductVariantDto ProductVariant { get; set; } = null!;

}
