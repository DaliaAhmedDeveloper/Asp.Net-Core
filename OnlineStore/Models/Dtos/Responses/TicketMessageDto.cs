namespace OnlineStore.Models.Dtos.Responses;
public class TicketMessageDto
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public int UserId { get; set; }
    public string Message { get; set; } = null!;
    public bool IsFromStaff { get; set; }
    public DateTime CreatedAt { get; set; }
}
