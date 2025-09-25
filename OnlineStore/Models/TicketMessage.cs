namespace OnlineStore.Models;

public class TicketMessage : SoftDeleteEntity
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public int? UserId { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsFromStaff { get; set; }
    public bool IsInternal { get; set; } // Staff-only notes
    public bool IsRead { get; set; }
    public DateTime? ReadAt { get; set; }

    // Navigation properties
    public SupportTicket Ticket { get; set; } = null!;
    public User User { get; set; } = null!;
} 