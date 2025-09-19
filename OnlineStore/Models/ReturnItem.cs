namespace OnlineStore.Models;
public class ReturnItem
{
    public int Id { get; set; }
    public int ReturnId { get; set; } // FK to Return
    public Return Return { get; set; } = null!;
    public int OrderItemId { get; set; } // FK to Product
    public OrderItem OrderItem { get; set; } = null!;
    public string? Reason { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal => Quantity * UnitPrice;
    public ICollection<ReturnAttachment> Attachments { get; set; } = new List<ReturnAttachment>();
}