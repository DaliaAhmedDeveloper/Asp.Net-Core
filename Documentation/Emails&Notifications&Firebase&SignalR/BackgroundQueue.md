# What is a Background Queue?

A Background Queue is a mechanism to enqueue work items (tasks/jobs) that need to be processed in the background, separately from handling incoming HTTP requests.

Instead of doing heavy or slow work directly inside a controller or user request, you push the work to a queue, and a background worker picks up the queued tasks to process them asynchronously.

This helps keep the application responsive and scalable.

# How does it relate to BackgroundService?

Typically, a BackgroundService acts as a worker that processes queued tasks from the Background Queue.

The queue holds the jobs; the BackgroundService dequeues and executes them.

This decouples task submission (e.g., an HTTP request adds a task) from task execution (BackgroundService runs the task later).

# Why use Background Queues?

To avoid blocking or slowing down HTTP requests by doing long-running work synchronously.

To improve reliability and retry mechanisms (you can retry failed jobs from the queue).

To scale background processing independently.

To handle spikes in workload smoothly by buffering tasks.

# How to implement Background Queue in ASP.NET Core?

1. Define a Queue Interface

``` csharp
public interface IBackgroundTaskQueue
{
    void Enqueue(Func<CancellationToken, Task> workItem);
    Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
}
```
2. Implement the Queue

```csharp

public class BackgroundTaskQueue : IBackgroundTaskQueue
{
    private readonly Channel<Func<CancellationToken, Task>> _queue = Channel.CreateUnbounded<Func<CancellationToken, Task>>();

    public void Enqueue(Func<CancellationToken, Task> workItem)
    {
        if (workItem == null) throw new ArgumentNullException(nameof(workItem));
        _queue.Writer.TryWrite(workItem);
    }

    public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
    {
        var workItem = await _queue.Reader.ReadAsync(cancellationToken);
        return workItem;
    }
}
```
3. Create a BackgroundService to process the queue

```csharp

public class QueuedHostedService : BackgroundService
{
    private readonly IBackgroundTaskQueue _taskQueue;

    public QueuedHostedService(IBackgroundTaskQueue taskQueue)
    {
        _taskQueue = taskQueue;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var workItem = await _taskQueue.DequeueAsync(stoppingToken);

            try
            {
                await workItem(stoppingToken);
            }
            catch (Exception ex)
            {
                // log the error
            }
        }
    }
}
```
4. Enqueue work items from controllers or services

```csharp

public class SomeController : ControllerBase
{
    private readonly IBackgroundTaskQueue _taskQueue;

    public SomeController(IBackgroundTaskQueue taskQueue)
    {
        _taskQueue = taskQueue;
    }

    [HttpPost("start-job")]
    public IActionResult StartJob()
    {
        _taskQueue.Enqueue(async token =>
        {
            // Your background job logic here
            await Task.Delay(10000, token); // simulate work
        });

        return Accepted();
    }
}
```