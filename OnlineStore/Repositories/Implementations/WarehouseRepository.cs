namespace OnlineStore.Repositories;
using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;
public class WarehouseRepository : GenericRepository<Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(AppDbContext context) : base(context)
    {
    }

      // get all with pagination 
    public virtual async Task<IEnumerable<Warehouse>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {
        if (!string.IsNullOrEmpty(searchTxt))
            return await _context.Warehouses.Where(wh => wh.Name.Contains(searchTxt)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await _context.Warehouses.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }
}
