using OnlineStore.Models.Enums;

namespace OnlineStore.Models;

public class Return : BaseEntity
{
    public int Id { get; set; }

    public int OrderId { get; set; } // FK to Order
    public Order Order { get; set; } = null!;
    public int? UserId { get; set; } 
    public decimal TotalAmount { get; set; }
    public string ReferenceNumber { get; set; } = Guid.NewGuid().ToString("N");
    public string? Reason { get; set; }
    public ReturnStatus Status { get; set; }
    public DateTime ReturnDate { get; set; } = DateTime.UtcNow;
    public ICollection<ReturnItem> ReturnItems { get; set; } = new List<ReturnItem>();
    public RefundType RefundType { set; get; } // wallet , card
    public ReturnTracking ReturnTracking { get; set; } = null!;
}
