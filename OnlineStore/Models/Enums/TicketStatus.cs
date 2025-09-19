namespace OnlineStore.Models.Enums;

public enum TicketStatus
{
    Open = 0,             // Ticket opened
    Assigned = 1,         // Ticket assigned to staff
    InProgress = 2,       // Ticket being worked on
    WaitingForCustomer = 3, // Waiting for customer response
    Resolved = 4,         // Ticket resolved
    Closed = 5,           // Ticket closed
    Cancelled = 6         // Ticket cancelled
} 