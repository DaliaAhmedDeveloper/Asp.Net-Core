using OnlineStore.Services;
public class DbLoggerProvider : ILoggerProvider
{
private readonly IServiceProvider _serviceProvider;
    public DbLoggerProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new DbLoggerService(_serviceProvider, categoryName);
    }

    public void Dispose() { }
}
