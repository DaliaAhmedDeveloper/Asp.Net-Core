namespace OnlineStore.Notifications;
using OnlineStore.Models;
using OnlineStore.Models.Enums;

public class PlaceOrderUserNotification
{
    public static Notification Build(int userId, string ReferenceNumber)
    {
        Notification notification = new Notification
        {
            Type = NotificationType.Info,
            Url = "api/profile/orders",
            UserId = userId,
            Translations = new List<NotificationTranslation>()
            {
                new NotificationTranslation {
                    LanguageCode = "en",
                    Title = "Order Received",
                    Message = $"We Received Your Order With Number {ReferenceNumber}"
                },
                new NotificationTranslation
                {
                    LanguageCode = "ar",
                    Title = "تم استلام الطلب",
                    Message = $"لقد استلمنا طلبك بالرقم {ReferenceNumber}"
                }

            }
        };

        return notification;
    }
}