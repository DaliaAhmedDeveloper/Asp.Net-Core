namespace OnlineStore.Repositories;
using OnlineStore.Models;
public interface IProductVariantRepository : IGenericRepository<ProductVariant>
{
    Task<IEnumerable<ProductVariant>> GetAllWithPaginationAsync(string searchTxt, int page, int pageSize);
    Task AddPivotAsync(VariantAttributeValue model);
    Task<ProductVariant?> GetDefaultVariantAsync(int productId);
    Task<IEnumerable<ProductVariant>> GetByProductIdAsync(int productId, bool? tracking = false);
    Task<ProductVariant?> GetByIdWithAvaAsync(int id ,  bool? tracking = false);
}