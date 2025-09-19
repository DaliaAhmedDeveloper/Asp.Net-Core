namespace OnlineStore.Notifications;

using OnlineStore.Models;
using OnlineStore.Models.Enums;

public class ReviewAdminNotification
{
    public static Notification Build(int adminUserId, Product product, int reviewId)
    {
        Notification notification = new Notification
        {
            Type = NotificationType.Info,
            Url = "dashboard/reviews/" + reviewId, // Admin dashboard page for reviews
            UserId = adminUserId,      // The admin's user ID
            Translations = new List<NotificationTranslation>()
            {
                new NotificationTranslation
                {
                    LanguageCode = "en",
                    Title = "New Review Submitted",
                    Message = $"A new review has been submitted for product {product.Slug}. Please check the dashboard to approve it."
                },
                new NotificationTranslation
                {
                    LanguageCode = "ar",
                    Title = "تم تقديم تقييم جديد",
                    Message = $"تم تقديم تقييم جديد للمنتج {product.Slug}. يرجى مراجعة لوحة التحكم للموافقة عليه."
                }
            }
        };

        return notification;
    }
}