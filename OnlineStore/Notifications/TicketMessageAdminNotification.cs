namespace OnlineStore.Notifications;

using OnlineStore.Models;
using OnlineStore.Models.Enums;

public class TicketMessageAdminNotification
{
    public static Notification Build(int adminUserId, SupportTicket ticket , User user)
    {
        Notification notification = new Notification
        {
            Type = NotificationType.Info,
            Url = "dashboard/suppost-ticket/" + ticket.Id, // Admin dashboard page for the ticket
            UserId = adminUserId, // The admin's user ID
            Translations = new List<NotificationTranslation>()
            {
                new NotificationTranslation
                {
                    LanguageCode = "en",
                    Title = "New Ticket Message",
                    Message = $"A new message has been added to ticket #{ticket.TicketNumber} by {user.FullName ?? "a user"}."
                },
                new NotificationTranslation
                {
                    LanguageCode = "ar",
                    Title = "رسالة جديدة في التذكرة",
                    Message = $"تمت إضافة رسالة جديدة إلى التذكرة رقم #{ticket.TicketNumber} بواسطة {user.FullName ?? "مستخدم"}."
                }
            }
        };
        return notification;
    }
}