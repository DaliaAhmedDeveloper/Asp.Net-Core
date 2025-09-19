namespace OnlineStore.Repositories;
using OnlineStore.Models;
public interface IProductAttributeValueRepository : IGenericRepository<AttributeValue>
{
    Task<IEnumerable<AttributeValue>> GetAllWithPaginationAsync(string searchTxt , int page , int pageSize);
}