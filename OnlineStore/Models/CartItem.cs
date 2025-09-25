namespace OnlineStore.Models;
public class CartItem : BaseEntity
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int CartId { get; set; }
    public Cart Cart { get; set; } = null!;
    public int VariantId { get; set; }
    public ProductVariant ProductVariant { get; set; } = null!; // has one ProductVariant
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!; // has one Product
}
