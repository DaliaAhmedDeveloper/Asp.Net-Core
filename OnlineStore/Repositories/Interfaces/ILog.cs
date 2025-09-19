namespace OnlineStore.Repositories;
using OnlineStore.Models;
public interface ILogRepository : IGenericRepository<Log>
{
    Task<IEnumerable<Log>> GetAllWithPaginationAsync(string searchTxt , int page , int pageSize);
}