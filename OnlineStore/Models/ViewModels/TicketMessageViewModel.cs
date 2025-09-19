namespace OnlineStore.Models.ViewModels;
public class TicketMessageViewModel
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public int UserId { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsFromStaff { get; set; }
}