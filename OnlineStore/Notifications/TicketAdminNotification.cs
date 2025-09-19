namespace OnlineStore.Notifications;

using OnlineStore.Models;
using OnlineStore.Models.Enums;

public class TicketAdminNotification
{
    public static Notification Build(int adminUserId, SupportTicket ticket , User user)
    {
        Notification notification = new Notification
        {
            Type = NotificationType.Info,
            Url = "dashboard/support-ticket/" + ticket.Id,
            UserId = adminUserId,
            Translations = new List<NotificationTranslation>()
            {
                new NotificationTranslation
                {
                    LanguageCode = "en",
                    Title = "New Support Ticket Submitted",
                    Message = $"A new support ticket (#{ticket.TicketNumber}) has been submitted by {user.FullName ?? "a user"}. Please check the dashboard."
                },
                new NotificationTranslation
                {
                    LanguageCode = "ar",
                    Title = "تم تقديم تذكرة دعم جديدة",
                    Message = $"تم تقديم تذكرة دعم جديدة (#{ticket.TicketNumber}) بواسطة {user.FullName ?? "مستخدم"}. يرجى مراجعة لوحة التحكم."
                }
            }
        };

        return notification;
    }
}