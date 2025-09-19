namespace OnlineStore.Services;
using OnlineStore.Helpers;
public class LanguageService : ILanguageService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LocalizationHelper _localizationHelper;

    public LanguageService(IHttpContextAccessor httpContextAccessor, LocalizationHelper localizationHelper)
    {
        _httpContextAccessor = httpContextAccessor;
        _localizationHelper = localizationHelper;
    }

    public string GetCurrentLanguage()
    {
        return _localizationHelper.GetPreferredLanguage(_httpContextAccessor);
    }
}
