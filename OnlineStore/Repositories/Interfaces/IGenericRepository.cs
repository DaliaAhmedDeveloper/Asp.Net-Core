namespace OnlineStore.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
    Task<T?> GetWithTranslationsAsync(int id);
    Task<IEnumerable<T>> GetAllWithTranslationsAsync();
    Task<int> CountAllAsync();
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<T>> GetAllWithTranslationsAndPaginationAsync(int page, int pageSize);
    Task<IEnumerable<T>> GetLatestAsync();
    Task<int> CountCurrentMonthAsync();
} 