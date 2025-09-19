using OnlineStore.Models.Enums;

namespace OnlineStore.Models.Dtos.Responses;
public class SupportTicketDto
{
    public int Id { get; set; }
    public string TicketNumber { get; set; } = null!;
    public int UserId { get; set; }
    public int? OrderId { get; set; }
    public TicketStatus Status { get; set; }
    public  TicketCategory Category { get; set; }
    public string Subject { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<TicketMessageDto> Messages { get; set; } = new List<TicketMessageDto>();
}
