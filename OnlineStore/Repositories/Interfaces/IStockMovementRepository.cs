namespace OnlineStore.Repositories;
using OnlineStore.Models;
public interface IStockMovementRepository : IGenericRepository<StockMovement>
{
    Task<IEnumerable<StockMovement>> GetAllWithPaginationAsync(string searchTxt, int page, int pageSize);
}