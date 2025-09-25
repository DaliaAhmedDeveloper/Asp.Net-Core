namespace OnlineStore.Repositories;

using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using OnlineStore.Services;

public class SupportTicketRepository : GenericRepository<SupportTicket>, ISupportTicketRepository
{
    private readonly IQueryService _query;
    public SupportTicketRepository(AppDbContext context, IQueryService query) : base(context)

    {
        _query = query;
    }
    // get order tickets
    public async Task<IEnumerable<SupportTicket>> GetByOrder(int orderId)
    {
        return await _context.SupportTickets.Where(st => st.OrderId == orderId).Include(st => st.Messages).ToListAsync();
    }

    // get user tickets
    public async Task<IEnumerable<SupportTicket>> GetByUser(int userId)
    {
        return await _context.SupportTickets
                             .Where(st => st.UserId == userId)
                             .Include(st => st.Messages)
                             .ToListAsync();
    }

    // web
    //get for web with relations
    public async Task<SupportTicket?> GetByIdWithRelationsAsync(int id)
    {
        return await _context.SupportTickets
        .Include(st => st.User)
        .FirstOrDefaultAsync(st => st.Id == id);
     }

    // get all with pagination 
    public async Task<IEnumerable<SupportTicket>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {
        if (!string.IsNullOrEmpty(searchTxt))
            return await _context.SupportTickets.Where(sm => sm.Description.Contains(searchTxt)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await _context.SupportTickets.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }
}


