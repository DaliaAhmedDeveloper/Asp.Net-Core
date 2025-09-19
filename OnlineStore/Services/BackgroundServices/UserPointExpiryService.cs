namespace OnlineStore.Services.BackgroundServices;

using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using OnlineStore.Models.Enums;
using OnlineStore.Services;

public class UserPointExpiryService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public UserPointExpiryService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var _language = scope.ServiceProvider.GetRequiredService<ILanguageService>();
                DateTime now = DateTime.UtcNow;
                var userPoints = await _context.UserPoints.Where(up => up.ExpiryAt < now).ToListAsync();

                foreach (var point in userPoints)
                {
                    point.User.UserAvailablePoints -= point.Points;
                    int userId = point.UserId;
                    point.Expired = true;
                    var Notification = new Notification
                    {
                        Type = NotificationType.Warning,
                        Url = "/points",
                        UserId = point.UserId,
                        Translations = new List<NotificationTranslation>
                        {
                            new NotificationTranslation
                            {
                                Title = "Points Expiration",
                                Message = $"{point.Points} Points Are About To Expire",
                                LanguageCode = "en"
                            },
                            new NotificationTranslation
                            {
                                Title = "انتهاء صلاحية النقاط",
                                Message = $"{point.Points} نقطة على وشك الانتهاء",
                                LanguageCode = "ar"
                            }
                        }
                    };
                    // firebase

                    // send notification
                    _context.Notifications.Add(Notification);
                    await _context.SaveChangesAsync();
                }

            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
}