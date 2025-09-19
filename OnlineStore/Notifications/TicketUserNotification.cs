namespace OnlineStore.Notifications;

using OnlineStore.Models;
using OnlineStore.Models.Enums;

public class TicketUserNotification
{
    public static Notification Build(SupportTicket ticket)
    {
        Notification notification = new Notification
        {
            Type = NotificationType.Info,
            Url = "account/support-ticket/" + ticket.Id,
            UserId = ticket.UserId,
            Translations = new List<NotificationTranslation>()
            {
                new NotificationTranslation
                {
                    LanguageCode = "en",
                    Title = "Your Support Ticket Has Been Created",
                    Message = $"Your support ticket (#{ticket.TicketNumber ?? ticket.Id.ToString()}) has been successfully submitted. Our support team will get back to you soon."
                },
                new NotificationTranslation
                {
                    LanguageCode = "ar",
                    Title = "تم إنشاء تذكرتك بنجاح",
                    Message = $"تم إنشاء تذكرة الدعم الخاصة بك (#{ticket.TicketNumber ?? ticket.Id.ToString()}) بنجاح. سيتواصل معك فريق الدعم قريباً."
                }
            }
        };

        return notification;
    }
}
