using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Repositories;

public class ProductVariantRepository : GenericRepository<ProductVariant>, IProductVariantRepository
{
    public ProductVariantRepository(AppDbContext context) : base(context)
    {
    }
    // get all with pagination 
    public async Task<IEnumerable<ProductVariant>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {
        // if (!string.IsNullOrEmpty(searchTxt))
        //     return await _context.Variants.Where(v => v..Contains(searchTxt)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await _context.Variants.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    // add attribute and value in pivot table
    public async Task AddPivotAsync(VariantAttributeValue model)
    {
        await _context.VariantAttributeValues.AddAsync(model);
    }
    // get the default product Variant
    public async Task<ProductVariant?> GetDefaultVariantAsync(int productId)
    {
        return await _context.Variants.FirstOrDefaultAsync(v => v.ProductId == productId && v.IsDefault == true);
    }
    // get all by product id
    public async Task<IEnumerable<ProductVariant>> GetByProductIdAsync(int productId, bool? tracking = false)
    {
        IQueryable<ProductVariant> query = _context.Variants;
        if (tracking == true)
        {
            query = query.AsTracking();
        }
        return await
        query
        .Where(v => v.ProductId == productId)
        .Where(v => v.IsDefault != true)
        .Include(v => v.VariantAttributeValues)
        .ThenInclude(vav => vav.Attribute)
        .Include(v => v.VariantAttributeValues)
        .ThenInclude(vav => vav.AttributeValue)
        .ToListAsync();
    }

    // GetByIdWithAvaAsync
    public async Task<ProductVariant?> GetByIdWithAvaAsync(int id , bool? tracking = false)
    {
        IQueryable<ProductVariant> query = _context.Variants;
        if (tracking == true)
        {
           query =  query.AsTracking();
        }
        return await query
        .Include(v => v.Stock)
        .Include(v => v.VariantAttributeValues)
        .FirstOrDefaultAsync(v => v.Id == id);
    }
      
}
