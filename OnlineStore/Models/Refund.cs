using OnlineStore.Models.Enums;

namespace OnlineStore.Models;

public class Refund : BaseEntity
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ReturnId { get; set; }
    public decimal Amount { get; set; }
    public RefundType Type { get; set; }
    public string Reason { get; set; } = string.Empty;
    public RefundStatus Status { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? ReferenceNumber { get; set; }
    public string? Notes { get; set; }
    
    // Navigation properties
    public Order Order { get; set; } = null!;
    public Return Return { get; set; } = null!;
} 