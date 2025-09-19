namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Models.ViewModels;

public interface ITicketMessageService
{
    // api
    Task<IEnumerable<TicketMessage>> ListByTicket(int ticketId);
    Task<TicketMessage> CreateTicketMessageAsync(CreateTicketMessageDto dto);

    // web
    Task<TicketMessage> CreateForWeb(TicketMessageViewModel model);
    Task<TicketMessage?> GetForWeb(int id);
    Task<PagedResult<TicketMessage>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber, int pageSize);
    Task<IEnumerable<TicketMessage>> GetAllForWeb();
    Task<IEnumerable<TicketMessage>> GetByTicketForWeb(int id);
    Task<TicketMessage> UpdateForWeb(TicketMessageViewModel model, TicketMessage TicketMessage);
    Task<bool> DeleteForWeb(int id);
}