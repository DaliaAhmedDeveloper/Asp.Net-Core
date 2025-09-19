namespace OnlineStore.Notifications;

using OnlineStore.Models;
using OnlineStore.Models.Enums;

public class ReturnRequestUserNotification
{
    public static Notification Build(int userId, string referenceNumber)
    {
        Notification notification = new Notification
        {
            Type = NotificationType.Info,
            Url = "api/profile/orders",
            UserId = userId,
            Translations = new List<NotificationTranslation>()
            {
              new NotificationTranslation
                {
                    LanguageCode = "en",
                    Title = "Your Return Request Received",
                    Message = $"Your return request with Number {referenceNumber} has been successfully submitted."
                },
                new NotificationTranslation
                {
                    LanguageCode = "ar",
                    Title = "تم استلام طلب الإرجاع الخاص بك",
                    Message = $"تم تقديم طلب الإرجاع الخاص بك بالرقم {referenceNumber} بنجاح."
                }
            }
        };

        return notification;
    }
}