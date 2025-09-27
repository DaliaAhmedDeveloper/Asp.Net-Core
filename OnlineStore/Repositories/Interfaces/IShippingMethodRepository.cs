namespace OnlineStore.Repositories;
using OnlineStore.Models;
public interface IShippingMethodRepository : IGenericRepository<ShippingMethod>
{
       Task<IEnumerable<ShippingMethod>> GetAllWithPaginationAsync(string searchTxt ,int pageNumber,int pageSize);

}