using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
namespace OnlineStore.Repositories;

public class StockRepository : GenericRepository<Stock>, IStockRepository
{
    public StockRepository(AppDbContext context) : base(context)
    {

    }
    // get stock if contain
    public async Task<IEnumerable<Stock>> Contains(List<int> items)
    {
        return await _context.Stock
                .Where(s => items.Contains(s.Id))
                .ToListAsync();
    }
    // get by variant id
    public async Task<Stock> GetByVariantIdAsync(int variantId)
    {
        return await _context.Stock
                .Where(s => s.ProductVariantId == variantId).FirstAsync();
    }
    // get all with pagination 
    public  async Task<IEnumerable<Stock>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {
        return await _context.Stock.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    } 
}
