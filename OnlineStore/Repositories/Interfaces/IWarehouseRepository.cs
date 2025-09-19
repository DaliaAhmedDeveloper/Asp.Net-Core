namespace OnlineStore.Repositories;
using OnlineStore.Models;
public interface IWarehouseRepository : IGenericRepository<Warehouse>
{
    Task<IEnumerable<Warehouse>> GetAllWithPaginationAsync(string searchTxt , int page , int pageSize);
}