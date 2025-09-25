namespace OnlineStore.Repositories;

using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

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

    // delete ware house 
    public override async Task<bool> DeleteAsync(int id)
    {
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var warehouse = await _context.Warehouses.AsTracking().Where(w => w.Id == id).Include(w => w.Stocks).FirstOrDefaultAsync();
            if (warehouse == null)
                return false;

            var stocks = warehouse.Stocks;
            foreach (var stock in stocks)
            {
                stock.WarehouseId = 1;
            }

            _dbSet.Remove(warehouse);
            scope.Complete(); 
            
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
