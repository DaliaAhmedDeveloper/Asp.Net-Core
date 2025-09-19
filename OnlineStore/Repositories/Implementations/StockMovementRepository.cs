using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;
namespace OnlineStore.Repositories;

public class StockMovementRepository : GenericRepository<StockMovement>, IStockMovementRepository
{
    public StockMovementRepository(AppDbContext context) : base(context)
    {
    }  

    // get all with pagination 
    public  async Task<IEnumerable<StockMovement>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {
        if (!string.IsNullOrEmpty(searchTxt))
            return await _context.StockMovements.Where(s => s.Reference.Contains(searchTxt)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await _context.StockMovements.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }  
}
