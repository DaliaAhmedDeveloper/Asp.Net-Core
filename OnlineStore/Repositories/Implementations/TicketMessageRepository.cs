namespace OnlineStore.Repositories;

using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using OnlineStore.Services;

public class TicketMessageRepository : GenericRepository<TicketMessage>, ITicketMessageRepository
{
    public TicketMessageRepository(AppDbContext context) : base(context)
    {
    }
    // get ticket messages 
    public async Task<IEnumerable<TicketMessage>> GetByTicket(int ticketId)
    {
        return await _context.TicketMessages.Include(tm => tm.User).Where(tm => tm.TicketId == ticketId).ToListAsync();
    }

    // get all with pagination 
    public  async Task<IEnumerable<TicketMessage>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {
        if (!string.IsNullOrEmpty(searchTxt))
            return await _context.TicketMessages.Where(tm => tm.Message.Contains(searchTxt)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await _context.TicketMessages.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }
}
