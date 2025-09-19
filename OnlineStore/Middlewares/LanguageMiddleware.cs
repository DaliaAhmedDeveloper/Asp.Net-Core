namespace OnlineStore.Middlewares;
using Microsoft.Extensions.Options;
using OnlineStore.Helpers;
public class LanguageMiddleware 
{
    private readonly RequestDelegate _next; 
    private readonly AppSettings _settings;
    public LanguageMiddleware(RequestDelegate next, IOptions<AppSettings> options)
    {
        _next = next; // assign next to _next
        _settings = options.Value;
    }
    public async Task InvokeAsync(HttpContext context) 
    {
        //code before middleware
       // _lang = LocalizationHelper.GetPreferredLanguage(context);
       
       // assign _lang to a global var can i access in repositories

        await _next(context); 
    }
}
