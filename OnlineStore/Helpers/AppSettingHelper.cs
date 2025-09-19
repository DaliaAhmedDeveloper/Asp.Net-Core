namespace OnlineStore.Helpers;

using OnlineStore.Services;
public class AppSettingHelper
{
    private readonly IAppSettingService _setting;
    public AppSettingHelper(IAppSettingService setting)
    {
        _setting = setting;
    }

    public async Task<string> GetValue(string code)
    {
        var value = await _setting.GetValueByCode(code);
        if (value == null)
            throw new NotFoundException("Code Is Not Found");

        return value.Value;
    }
}