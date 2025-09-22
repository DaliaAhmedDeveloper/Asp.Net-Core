namespace OnlineStore.Services;
using OnlineStore.Models;
using OnlineStore.Helpers;

public class DbLoggerService : ILogger // class Dblogger implement Ilogger interface which is Responsible for logging messages
{
    private readonly IServiceProvider _serviceProvider;
    private string _categoryName;
    public DbLoggerService(IServiceProvider serviceProvider, string categoryName)
    {
        _serviceProvider = serviceProvider;
        _categoryName = categoryName;
    }

    public IDisposable BeginScope<TState>(TState state) => null!; // IDisposable free up the resouces after finish the scope

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= LogLevel.Warning; // log only warnings and errors
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId,
        TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        using (var scope = _serviceProvider.CreateScope())
        {
            var _dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            if (exception != null)
            {
                var log = new Log
                {
                    StackTrace = exception.StackTrace ?? string.Empty,
                    CreatedAt = DateTime.UtcNow
                };
                if (state is LoggerHelper.LogState myState)
                {
                    var logTranslations = new List<LogTranslation>{
                    new LogTranslation{
                        ExceptionMessage = myState.MessageEn,
                        ExceptionTitle = myState.TitleEn,
                        LanguageCode = "en",
                    },
                    new LogTranslation{
                        ExceptionMessage = myState.MessageAr,
                        ExceptionTitle = myState.TitleAr,
                        LanguageCode = "ar",
                    }
                };
                    log.Translations = logTranslations;

                    _dbContext.Logs.Add(log);
                }

                try
                {
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
