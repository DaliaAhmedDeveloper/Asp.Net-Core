namespace OnlineStore.Repositories;

using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using System.Threading.Tasks;

public class ReturnRepository : GenericRepository<Return>, IReturnRepository
{
    public ReturnRepository(AppDbContext context) : base(context)
    {

    }

    // get all with pagination 
    public async Task<IEnumerable<Return>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {
        IQueryable<Return> query = _context.Returns.OrderByDescending(r => r.ReturnDate);
        if (!string.IsNullOrEmpty(searchTxt))
            return await query.Where(r => r.ReferenceNumber.Contains(searchTxt)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }
    // get with relations
       public async Task<Return?> GetForWebWithRelationsAsync(int id)
    {
        return await _context.Returns
        .Where(r => r.Id == id)
        .Include(r => r.ReturnItems)
        .FirstOrDefaultAsync();
    }
}
