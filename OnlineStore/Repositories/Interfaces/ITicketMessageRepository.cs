namespace OnlineStore.Repositories;

using OnlineStore.Models;

public interface ITicketMessageRepository : IGenericRepository<TicketMessage>
{
     Task<IEnumerable<TicketMessage>> GetByTicket(int ticketId);
     Task<IEnumerable<TicketMessage>> GetAllWithPaginationAsync(string searchTxt , int page , int pageSize);
} 