namespace OnlineStore.Services;
using OnlineStore.Models;
using OnlineStore.Models.ViewModels;

public interface ILogService
{
    // api

     // web
    Task<Log> CreateForWeb(Log model);
    Task<Log?> GetForWeb(int id);
    Task<PagedResult<Log>> GetAllWithPaginationForWeb(string searchTxt , int pageNumber, int pageSize);
    Task<IEnumerable<Log>> GetAllForWeb();
    Task<bool> DeleteForWeb(int id);
} 