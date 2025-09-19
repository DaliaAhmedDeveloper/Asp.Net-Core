namespace OnlineStore.Notifications;
using OnlineStore.Models;
using OnlineStore.Models.Enums;

public class PlaceOrderAdminNotification
{
    public static Notification Build(int userId, string ReferenceNumber)
    {
        Notification notification = new Notification
        {
            Type = NotificationType.Info,
            Url = "dashboard/orders",
            UserId = userId,
            Translations = new List<NotificationTranslation>()
            {
                new NotificationTranslation{
                    LanguageCode = "en",
                    Title = "New Order Received",
                    Message = $"A new order has been placed with Number {ReferenceNumber} by User {userId}"
                },
                new NotificationTranslation{
                    LanguageCode = "ar",
                    Title = "تم استلام طلب جديد",
                    Message = $"تم تقديم طلب جديد بالرقم {ReferenceNumber} من قبل المستخدم {userId}"
                }
            }
        };

        return notification;
    }
}