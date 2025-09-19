using OnlineStore.Models;
using OnlineStore.Services.BackgroundServices;

public class QueuedHostedService : BackgroundService
{
    private readonly IBackgroundTaskQueue _taskQueue;
    private readonly IServiceProvider _serviceProvider;
    public QueuedHostedService(IBackgroundTaskQueue taskQueue, IServiceProvider serviceProvider)
    {
        _taskQueue = taskQueue;
        _serviceProvider = serviceProvider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var workItem = await _taskQueue.DequeueAsync(stoppingToken);

            int retries = 0;
            bool success = false;

            while (!success && retries < 3 && !stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await workItem(stoppingToken);
                    success = true;
                }
                catch (Exception ex)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var _dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                        var failedTask = new FailedTask
                        {
                            TaskName = ex.GetType().Name,
                            Details = ex.GetBaseException().Message,
                            ExceptionMessage = ex.Message,
                            StackTrace = ex.StackTrace,
                            Try = retries
                        };

                        _dbContext.FailedTasks.Add(failedTask);
                        _dbContext.SaveChanges();
                    }

                    retries++;
                    Console.WriteLine($"Task failed, attempt {retries}: {ex.Message}");
                    if (retries < 3)
                        await Task.Delay(2000 * retries, stoppingToken);
                    else
                        Console.WriteLine("Task failed after max retries.");
                }
            }
        }
    }
}
