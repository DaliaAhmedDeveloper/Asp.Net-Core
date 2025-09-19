namespace OnlineStore.Repositories;
using OnlineStore.Models;
public interface IAppSettingRepository : IGenericRepository<AppSetting>
{
    Task<AppSetting?> GetByCodeAsync(string code);
}