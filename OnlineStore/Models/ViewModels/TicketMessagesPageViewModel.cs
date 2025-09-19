namespace OnlineStore.Models.ViewModels;
public class TicketMessagesPageViewModel
{
    public SupportTicket? Ticket { get; set; } = default!;
    public TicketMessageViewModel Message { get; set; } = new();
}
