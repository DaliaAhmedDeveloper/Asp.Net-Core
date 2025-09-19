namespace OnlineStore.Notifications;

using OnlineStore.Models;
using OnlineStore.Models.Enums;

public class OrderStatusAdminNotification
{
    public static Notification Build(int userId, string ReferenceNumber, OrderStatus status)
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
                    Title = $"Order {status}",
                    Message = $"order with Number {ReferenceNumber} by User {userId} Status changes to {status}"
                },
               new NotificationTranslation{
                    LanguageCode = "ar",
                    Title = $"الطلب {status}",
                    Message = $"تم تغيير حالة الطلب رقم {ReferenceNumber} للمستخدم {userId} إلى {status}"
                },
            }
        };

        return notification;
    }
}