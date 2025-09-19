using OnlineStore.Models.Enums;
namespace OnlineStore.Models;
public class SupportTicket : BaseEntity
{
    public int Id { get; set; }
    public string TicketNumber { get; set; } = string.Empty;
    public int UserId { get; set; }
    public int OrderId { get; set; }
    public TicketPriority Priority { get; set; }
    public TicketStatus Status { get; set; }
    public TicketCategory Category { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? AssignedToUserId { get; set; }
    public DateTime? AssignedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }
    public string? Resolution { get; set; }
    
    // Navigation properties
    public User User { get; set; } = null!;
    public Order Order { get; set; } = null!;
    public ICollection<TicketMessage> Messages { get; set; } = new List<TicketMessage>();
} 