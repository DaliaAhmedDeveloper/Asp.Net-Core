namespace OnlineStore.Repositories;
using OnlineStore.Models;

public interface IReturnRepository : IGenericRepository<Return>
{

    // Task<Return?> GetWithItemsByIdAsync(int ReturnId);
    // Task<IEnumerable<Return>> GetAllByUserAsync(int userId);
    // Task<IEnumerable<Return>> GetAllCurrentMonthAsync();
    // Task<Return?> GetForWebWithRelationsAsync(int id);
    // Task<Return?> GetByReferenceNumberAsync(string ReferenceNumber);
    Task<IEnumerable<Return>> GetAllWithPaginationAsync(string searchTxt, int page, int pageSize);
    Task<Return?> GetForWebWithRelationsAsync(int id);
}
