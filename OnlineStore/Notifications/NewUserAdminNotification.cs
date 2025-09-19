namespace OnlineStore.Notifications;

using OnlineStore.Models;
using OnlineStore.Models.Enums;

public class NewUserAdminNotification
{
    public static Notification Build(int adminUserId, User newUser)
    {
        Notification notification = new Notification
        {
            Type = NotificationType.Info,
            Url = "dashboard/users/" + newUser.Id,
            UserId = adminUserId,
            Translations = new List<NotificationTranslation>()
            {
                new NotificationTranslation
                {
                    LanguageCode = "en",
                    Title = "New User Account Created",
                    Message = $"A new user account has been created for {newUser.FullName} ({newUser.Email}). Please review their profile."
                },
                new NotificationTranslation
                {
                    LanguageCode = "ar",
                    Title = "تم إنشاء حساب مستخدم جديد",
                    Message = $"تم إنشاء حساب مستخدم جديد لـ {newUser.FullName} ({newUser.Email}). يرجى مراجعة الملف الشخصي."
                }
            }
        };

        return notification;
    }
}
