namespace OnlineStore.Notifications;

using OnlineStore.Models;
using OnlineStore.Models.Enums;

public class OrderStatusUserNotification
{
    public static Notification Build(int userId, string ReferenceNumber, OrderStatus status)
    {
        var translations = new List<NotificationTranslation>()
            {
                new NotificationTranslation {
                    LanguageCode = "en",
                    Title = $"Order {status}",
                    Message = $"Your Order With Number {ReferenceNumber} Status Changed To {status}"
                },
                new NotificationTranslation {
                    LanguageCode = "ar",
                    Title = $"الطلب {status}",
                    Message = $"تم تغيير حالة طلبك رقم {ReferenceNumber} إلى {status}"
                },
            };

        if (status == OrderStatus.Delivered)
        {
            translations = new List<NotificationTranslation>()
            {
                new NotificationTranslation {
                    LanguageCode = "en",
                    Title = $"Order {status}",
                    Message = $"Your Order With Number {ReferenceNumber} Status Changed To {status} You Can Add Reviews Now"
                },
                new NotificationTranslation {
                    LanguageCode = "ar",
                    Title = $"الطلب {status}",
                    Message = $"تم تغيير حالة طلبك رقم {ReferenceNumber} إلى {status} ويمكنك الآن إضافة تقييمات"
                },
            };
        }
        Notification notification = new Notification
        {
            Type = NotificationType.Info,
            Url = "api/profile/orders",
            UserId = userId,
            Translations = translations
        };

        return notification;
    }
}