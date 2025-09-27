using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using OnlineStore.Services;

namespace OnlineStore.Repositories;

public class ShippingMethodRepository : GenericRepository<ShippingMethod>, IShippingMethodRepository
{
    public ShippingMethodRepository(AppDbContext context) : base(context)
    {
    }
     // get all with pagination 
    public  async Task<IEnumerable<ShippingMethod>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {
        if (!string.IsNullOrEmpty(searchTxt))
            return await _context.ShippingMethods.Include(s => s.Translations).Where(c => c.Name.Contains(searchTxt) || c.Translations.Any(ct => ct.Name.Contains(searchTxt))).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await _context.ShippingMethods.Include(c => c.Translations).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

}
