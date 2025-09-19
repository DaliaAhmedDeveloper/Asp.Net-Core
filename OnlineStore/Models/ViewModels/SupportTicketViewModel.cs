using OnlineStore.Models.Enums;

namespace OnlineStore.Models.ViewModels;

public class SupportTicketViewModel
{
    public int? Id { get; set; }
    public string TicketNumber { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string OrderNumber { get; set; } = string.Empty;
    public TicketPriority Priority { get; set; }
    public TicketStatus Status { get; set; }
    public TicketCategory Category { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AssignedUserName { get; set; } = string.Empty;
    public string AssignedUserEmail { get; set; } = string.Empty;
    public string? Resolution { get; set; }
    public int? AssignedToUserId { get; set; }
     
}