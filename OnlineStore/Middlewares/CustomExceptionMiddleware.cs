namespace OnlineStore.Middlewares;

using System.Text.Json;
using Microsoft.Extensions.Localization;
using OnlineStore.Helpers;
public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionMiddleware> _logger;
    private readonly IStringLocalizer<CustomExceptionMiddleware> _localizer;
    public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger, IStringLocalizer<CustomExceptionMiddleware> localizer)
    {
        _next = next;
        _logger = logger;
        _localizer = localizer;
    }

    public async Task Invoke(HttpContext context)
    {
        var api = context.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase);
        var dashboard = context.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase);

        try
        {
            await _next(context);
        }
        catch (ResponseErrorException ex)
        {
            _logger.LogError(ex, ex.Message);
            _logger.LogErrorWithTranslations( // log to the database
                ex,
                "Api Response Error",
                "An API response error occurred while running the application. Please check your request or server configuration",
                "خطأ في الاستجابة",
                "حدث خطأ في استجابة واجهة برمجة التطبيقات أثناء تشغيل التطبيق. يرجى التحقق من الطلب أو إعدادات الخادم."
            );
            if (api)
                await WriteErrorResponseAsync(context, 400, ex.Message);
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, "Not Found Object"); // log to the console and file 
            _logger.LogErrorWithTranslations( // log to the database
                ex,
               "Not Found Exception",
                "The requested object was not found.",
                "استثناء غير موجود",
                "العنصر المطلوب غير موجود"
            );
            if (api)
                await WriteErrorResponseAsync(context, 404, ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogError(ex, "Un authorized"); // log to the console and file 
            _logger.LogErrorWithTranslations( // log to the database
                ex,
               "Unauthorized Exception",
                "User not authorized to perform this action. Please log in with proper credentials or contact your administrator.",
                "استثناء غير مصرح",
                "أنت غير مصرح لك بتنفيذ هذا الإجراء. يرجى تسجيل الدخول باستخدام بيانات اعتماد صحيحة أو الاتصال بالمسؤول."
            );
            if (api)
                await WriteErrorResponseAsync(context, 401, "Un authorized", ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex, "Forbidden");// log to the console and file 
            _logger.LogErrorWithTranslations( // log to the database
                ex,
               "Forbidden Exception",
                "You do not have permission to access this resource. Please contact your administrator if you believe this is an error.",
                "استثناء مرفوض",
                "ليس لديك إذن للوصول إلى هذا المورد. يرجى الاتصال بالمسؤول إذا كنت تعتقد أن هذا خطأ."
            );
            if (api)
                await WriteErrorResponseAsync(context, 403, "Forbidden", ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            // log to the console and file 
            _logger.LogErrorWithTranslations( // log to the database
                ex,
                "Unhandled Exception",
                "An unexpected error occurred that was not handled by the application. Please try again or contact support.",
                "استثناء غير معالج",
                "حدث خطأ غير متوقع لم يتم التعامل معه بواسطة التطبيق. يرجى المحاولة مرة أخرى أو الاتصال بالدعم."
            );
            if (api)
                await WriteErrorResponseAsync(context, 500, _localizer["ErrorMessage"], ex.Message);            
        }
    }
    // handle exception json
    private async Task WriteErrorResponseAsync(HttpContext context, int statusCode, string message, string? data = null)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var response = new ApiResponseHelper<string>(
            status: false,
            statusCode: statusCode,
            message: message,
            data: data
        );
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
