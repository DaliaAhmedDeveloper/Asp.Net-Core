namespace OnlineStore.Services;
using OnlineStore.Models.ViewModels;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;

public interface ISupportTicketService
{
    // api
    Task<IEnumerable<SupportTicket>> ListByOrder(int orderId);
    Task<IEnumerable<SupportTicket>> ListByUser(int userId);
    Task<SupportTicket> CreateSupportTicketAsync(CreateSupportTicketDto dto);
     // web
    Task<SupportTicket> CreateForWeb(SupportTicketViewModel model);
    Task<SupportTicket?> GetForWeb(int id);
    Task<PagedResult<SupportTicket>> GetAllWithPaginationForWeb(string searchTxt , int pageNumber, int pageSize);
    Task<IEnumerable<SupportTicket>> GetAllForWeb();
    Task<SupportTicket> UpdateForWeb(SupportTicketViewModel model, SupportTicket SupportTicket);
    Task<bool> DeleteForWeb(int id);
} 