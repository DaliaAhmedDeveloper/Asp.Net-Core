using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;

namespace OnlineStore.Repositories;

public class ProductAttributeValueRepository : GenericRepository<AttributeValue>, IProductAttributeValueRepository
{
    public ProductAttributeValueRepository(AppDbContext context) : base(context)
    {
    }
    // get all with pagination 
    public  async Task<IEnumerable<AttributeValue>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var query = _context.AttributeValues.Include(av => av.Translations).Where(av => av.Attribute.IsDeleted == false);
        if (!string.IsNullOrEmpty(searchTxt))
            return await  query.Where(av => av.Code != null && av.Code.Contains(searchTxt) || av.Translations.Any(t => t.Name.Contains(searchTxt))).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await  query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

}
