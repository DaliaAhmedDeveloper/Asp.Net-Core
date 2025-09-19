namespace OnlineStore.Notifications;

using OnlineStore.Models;
using OnlineStore.Models.Enums;

public class WelcomeUserNotification
{
    public static Notification Build(int userId, User user)
    {
        Notification notification = new Notification
        {
            Type = NotificationType.Info, 
            Url = "dashboard",
            UserId = userId,
            Translations = new List<NotificationTranslation>()
            {
                new NotificationTranslation
                {
                    LanguageCode = "en",
                    Title = "Welcome to OnlineStore!",
                    Message = $"Hello {user.FullName}, welcome to OnlineStore! We’re excited to have you on board."
                },
                new NotificationTranslation
                {
                    LanguageCode = "ar",
                    Title = "مرحبًا بك في OnlineStore!",
                    Message = $"مرحبًا {user.FullName}، نرحب بك في OnlineStore! نحن سعداء بانضمامك."
                }
            }
        };

        return notification;
    }
}
