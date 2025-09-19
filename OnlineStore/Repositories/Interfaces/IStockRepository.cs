namespace OnlineStore.Repositories;
using OnlineStore.Models;
public interface IStockRepository : IGenericRepository<Stock>
{
    Task<IEnumerable<Stock>> Contains(List<int> items);
    Task<Stock> GetByVariantIdAsync(int variantId);
    Task<IEnumerable<Stock>> GetAllWithPaginationAsync(string searchTxt, int page, int pageSize);
}