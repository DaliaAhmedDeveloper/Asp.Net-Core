namespace OnlineStore.Repositories;
using OnlineStore.Models;
public interface IProductAttributeRepository : IGenericRepository<ProductAttribute>
{
    Task<IEnumerable<ProductAttribute>> GetAllWithPaginationAsync(string searchTxt, int page, int pageSize);

    Task<IEnumerable<AttributeValue>> GetValuesAsync(int attributeId);
}