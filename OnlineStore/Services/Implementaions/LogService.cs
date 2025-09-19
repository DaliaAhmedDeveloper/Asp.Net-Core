namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.ViewModels;
using OnlineStore.Repositories;

public class LogService : ILogService
{
    private readonly ILogRepository _logRepo;

    public LogService(ILogRepository logRepo)
    {
        _logRepo = logRepo;
    }

    /*=========== API ========================*/

    /*=========== WEB ========================*/
    // get
    public async Task<IEnumerable<Log>> GetAllForWeb()
    {
        return await _logRepo.GetAllAsync();
    }
    // get all with pagination
    public async Task<PagedResult<Log>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _logRepo.CountAllAsync();
        var logs = await _logRepo.GetAllWithPaginationAsync(searchTxt, pageNumber, pageSize);
        var model = new PagedResult<Log>
        {
            Items = logs,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }
    // get details
    public async Task<Log?> GetForWeb(int id)
    {
        return await _logRepo.GetWithTranslationsAsync(id);
    }

    // add new Log
    public async Task<Log> CreateForWeb(Log model)
    {
        var log = new Log
        {
           
            Translations = new List<LogTranslation>
            {
                new LogTranslation { LanguageCode = "en"},
                new LogTranslation { LanguageCode = "ar" }
            }
        };

        await _logRepo.AddAsync(log);
        return log;
    }
    // delete Log
    public async Task<bool> DeleteForWeb(int id)
    {
        return await _logRepo.DeleteAsync(id);
    }
   
} 