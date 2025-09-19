namespace OnlineStore.Notifications;

using OnlineStore.Models;
using OnlineStore.Models.Enums;

public class TicketMessageUserNotification
{
    public static Notification Build(int userId , SupportTicket ticket)
    {
        Notification notification = new Notification
        {
            Type = NotificationType.Info,
            Url = "account/support-ticket/" + ticket.Id,
            UserId = userId,
            Translations = new List<NotificationTranslation>()
            {
                new NotificationTranslation 
                {
                    LanguageCode = "en",
                    Title = "New Reply to Your Support Ticket",
                    Message = $"A new reply has been added to your support ticket (#{ticket.TicketNumber ?? ticket.Id.ToString()}) by our support team."
                },
                new NotificationTranslation
                {
                    LanguageCode = "ar",
                    Title = "رد جديد على تذكرتك",
                    Message = $"تمت إضافة رد جديد على تذكرة الدعم الخاصة بك (#{ticket.TicketNumber ?? ticket.Id.ToString()}) من فريق الدعم."
                }
            }
        };

        return notification;
    }
}