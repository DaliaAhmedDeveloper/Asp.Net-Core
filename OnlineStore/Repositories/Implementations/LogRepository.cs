using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;
namespace OnlineStore.Repositories;

public class LogRepository : GenericRepository<Log>, ILogRepository
{
    public LogRepository(AppDbContext context) : base(context)
    {
    }

    // get all with pagination 
    public  async Task<IEnumerable<Log>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {
        if (!string.IsNullOrEmpty(searchTxt))
            return await _context.Logs.Include(l=>l.Translations).Where(l => l.StackTrace.Contains(searchTxt)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await _context.Logs.Include(l=>l.Translations).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    } 

}
