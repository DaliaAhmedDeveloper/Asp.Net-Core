namespace OnlineStore.Helpers;

using System.Globalization;
using Microsoft.Extensions.Options;
public class LocalizationHelper
{
    public  readonly string _langDefault;
    public LocalizationHelper(IOptions<AppSettings> options)
    {
        _langDefault = options.Value.Language;
    }
    public string GetPreferredLanguage(IHttpContextAccessor httpContextAccessor)
    {
        var context = httpContextAccessor.HttpContext;
        var apiLang = context?.Request.Headers.AcceptLanguage.ToString();
        var webLang = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        bool? isApi = context?.Request.Path.StartsWithSegments("/api");

        if (isApi != null && isApi == true && !string.IsNullOrWhiteSpace(apiLang))
        {
            // Get the first preferred language (e.g. "en-US,en;q=0.9")
            var languages = apiLang.Split(',')
                                      .Select(l => l.Split(';').First())
                                      .ToList();
            if (languages.Count != 0)
                return languages.First().Split('-')[0]; // "en-US" => "en"
        }
        else if (!string.IsNullOrWhiteSpace(webLang))
        {
            return webLang;
        }
        return _langDefault;
    }
}
