namespace OnlineStore.Notifications;
using OnlineStore.Models;
using OnlineStore.Models.Enums;

public class ReturnRequestAdminNotification
{
    public static Notification Build(int userId, string referenceNumber)
    {
        Notification notification = new Notification
        {
            Type = NotificationType.Info,
            Url = "dashboard/orders",
            UserId = userId,
            Translations = new List<NotificationTranslation>()
            {
                new NotificationTranslation
                {
                    LanguageCode = "en",
                    Title = "New Return Request Received",
                    Message = $"A new return request has been placed with Number {referenceNumber} by User {userId}"
                },
                new NotificationTranslation
                {
                    LanguageCode = "ar",
                    Title = "تم استلام طلب إرجاع جديد",
                    Message = $"تم تقديم طلب إرجاع جديد بالرقم {referenceNumber} من قبل المستخدم {userId}"
                }
            }
        };

        return notification;
    }
}