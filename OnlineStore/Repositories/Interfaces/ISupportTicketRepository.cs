namespace OnlineStore.Repositories;

using OnlineStore.Models;

public interface ISupportTicketRepository : IGenericRepository<SupportTicket>
{
    Task<IEnumerable<SupportTicket>> GetByOrder(int orderId);
    Task<IEnumerable<SupportTicket>> GetByUser(int userId);
    // web
    Task<SupportTicket?> GetByIdWithRelationsAsync(int id);
    Task<IEnumerable<SupportTicket>> GetAllWithPaginationAsync(string searchTxt , int page , int pageSize);
} 