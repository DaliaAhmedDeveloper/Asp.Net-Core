namespace OnlineStore.Services;
using Microsoft.Extensions.Options;
using OnlineStore.Models;
using OnlineStore.Repositories;

public class AppSettingService : IAppSettingService
{
    private readonly IAppSettingRepository _settings;
    public AppSettingService(IAppSettingRepository settings)
    {
        _settings = settings;
    }

    public async Task<bool> Update(Dictionary<int, string> settings)
    {
        if (settings != null && settings.Any())
        {
            foreach (var setting in settings)
            {
                var entity = await _settings.GetByIdAsync(setting.Key);
                if (entity != null)
                {
                    entity.Value = setting.Value;
                    await _settings.UpdateAsync(entity);
                }
            }
        }
        return true;
    }
    public async Task<IEnumerable<AppSetting>> List()
    {
        return await _settings.GetAllWithTranslationsAsync();
    }
    // get by code 
    public async Task<AppSetting?> GetValueByCode(string code)
    {
        return await _settings.GetByCodeAsync(code);
     }

}