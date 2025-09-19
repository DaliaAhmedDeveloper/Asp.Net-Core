namespace OnlineStore.Repositories;

using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;
public class ProductAttributeRepository : GenericRepository<ProductAttribute>, IProductAttributeRepository
{
    public ProductAttributeRepository(AppDbContext context) : base(context)
    {
    }
    // get all with pagination 
    public async Task<IEnumerable<ProductAttribute>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {
        if (!string.IsNullOrEmpty(searchTxt))
            return await _context.ProductAttributes.Include(pa => pa.Translations).Where(pa => pa.Code != null && pa.Code.Contains(searchTxt) || pa.Translations.Any(t => t.Name.Contains(searchTxt))).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await _context.ProductAttributes.Include(c => c.Translations).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    //  get valuses
   public async Task<IEnumerable<AttributeValue>> GetValuesAsync(int attributeId)
    {
        return await _context.AttributeValues.Where(av => av.AttributeId  == attributeId).ToListAsync();
    }

}
