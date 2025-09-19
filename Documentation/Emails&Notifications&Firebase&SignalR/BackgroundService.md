# Background Service In ASP.NET Core 8

## What is Background Service ? 

A Background Service is a piece of code that runs continuously in the background of an application, independently of user requests.

## How to use ?

Create a service 

```csharp
public class CartReminderService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public CartReminderService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try{
                    var dbContext = scope.ServiceProvider.GetRequiredService<YourDbContext>();
                    var cartsToNotify = await dbContext.Carts
                        .Where(c => c.Items.Any() && c.LastUpdated >= DateTime.UtcNow.AddHours(-1))
                        .ToList();

                    foreach (var cart in cartsToNotify)
                    {
                        // Send email and Firebase notification here
                    }
                }
                catch (Exception ex)
                {
                    // log the exception
                }
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
}
// register in program.cs 
builder.Services.AddHostedService<CartReminderService>();
```

Lets analyze this code 
1. The class inherits from BackgroundService, which is a base class provided by .NET to create hosted background tasks.
   its abstract class contains abstract method called ExcuteAsync you have to ovveride ,, and some other methods inside it 
   Help with run the task ( your logic ) in background 

2. IServiceProvider is inject inside the task service why ??
   becaause of the  BackgroundService class is singlton inside DI becuse it will run only one time 
   so not able to inject all other scoped classes direct inside it 
   IServiceProvider helps with that it allow u to use scoped classes if u need like dbcontext which is use to work with database

   also provide method to create scope to contain your logic -- this scope like a loop for your login every specific time you will configure 

3. !stoppingToken.IsCancellationRequested : this means while the application is working 
4. await Task.Delay(TimeSpan.FromHours(1), stoppingToken) : 
   it means Pause this background loop for 1 hour before running it again — but if the application is shutting down, stop waiting immediately.

## Explanation of why to use !stoppingToken.IsCancellationRequested :

In a background service, this check helps the application know when it should stop running, such as when the app is shutting down or restarting.

When you use Task.Delay with a long delay (e.g., one hour), it creates an object in memory that represents this waiting period.

You check !stoppingToken.IsCancellationRequested inside the loop so that when a shutdown request comes, you don’t enter a new iteration of the loop. Instead, you stop immediately instead of repeating work or waiting longer. Because There is a period of time between the shutdown request and the actual shutdown of the application.

BackgroundService class will end all delays immediately when Asp.net send there is shutdown request and free all resource (objects in memory)
