namespace OnlineStore.Models;

public class OrderItem
{
    public int Id { get; set; } 
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int ProductVariantId { get; set; }
    public ProductVariant ProductVariant { get; set; } = null!;
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }

    // Number Of Points Applied
    public int Points { get; set; }

    // Wallet Amount Applied
    public decimal WalletAmount { get; set; }
    public bool IsReviewed { get; set; }
    public bool IsReturned { get; set; }
    public ReturnItem? ReturnItem { get; set; }
}
