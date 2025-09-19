namespace OnlineStore.Services;

using OnlineStore.Models;

public interface IAppSettingService
{
    Task<bool> Update(Dictionary<int, string> settings);
    Task<IEnumerable<AppSetting>> List();
    Task<AppSetting?> GetValueByCode(string code);
}