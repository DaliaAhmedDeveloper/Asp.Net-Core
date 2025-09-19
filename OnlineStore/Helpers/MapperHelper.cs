using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;

namespace OnlineStore.Helpers;

public static class MapperHelper
{
    public static SupportTicketDto ToDto(this SupportTicket ticket) // extention method
    {
        if (ticket == null) return null!;

        return new SupportTicketDto
        {
            Id = ticket.Id,
            TicketNumber = ticket.TicketNumber,
            UserId = ticket.UserId,
            OrderId = ticket.OrderId,
            Status = ticket.Status,
            Category = ticket.Category,
            Subject = ticket.Subject,
            Description = ticket.Description,
            Messages = ticket.Messages?.Select(m => new TicketMessageDto
            {
                Id = m.Id,
                TicketId = m.TicketId,
                UserId = m.UserId,
                Message = m.Message,
                IsFromStaff = m.IsFromStaff,
                CreatedAt = m.CreatedAt
            }).ToList() ?? new List<TicketMessageDto>()
        };
    }

    public static List<SupportTicketDto> TicketList(this IEnumerable<SupportTicket> tickets)
    {
        return tickets.Select(t => t.ToDto()).ToList();
    }
    public static TicketMessageDto ToDto(this TicketMessage m) // extention method
    {
        if (m == null) return null!;

        return new TicketMessageDto
        {
            Id = m.Id,
            TicketId = m.TicketId,
            UserId = m.UserId,
            Message = m.Message,
            IsFromStaff = m.IsFromStaff,
            CreatedAt = m.CreatedAt
        };
    }
     public static List<TicketMessageDto> TicketMessageList(this IEnumerable<TicketMessage> messages)
    {
        return messages.Select(t => t.ToDto()).ToList();
    }
}
