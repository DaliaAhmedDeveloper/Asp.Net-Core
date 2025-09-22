namespace OnlineStore.Helpers;

using Microsoft.IdentityModel.Tokens;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.Enums;
using OnlineStore.Notifications;
using OnlineStore.Repositories;
using OnlineStore.Services;
public class ReturnHelper
{
    private readonly IEmailService _email;
    private readonly INotificationRepository _notificationRepo;
    private readonly IUserRepository _userRepo;
    private readonly AppSettingHelper _setting;
    private readonly PushNotificationHelper _push;
    public ReturnHelper(
        IEmailService email,
        INotificationRepository notificationRepo,
        AppSettingHelper setting,
        PushNotificationHelper push,
        IUserRepository userRepo
    )
    {
        _email = email;
        _notificationRepo = notificationRepo;
        _setting = setting;
        _push = push;
        _userRepo = userRepo;
    }
    /*
    Send Order Related Notifications
    */
    public async Task SendOrderNotifications(int userId, Return _return)
    {
        // Notifications to user
        var referenceNumber = _return.ReferenceNumber;
        var userNotification = ReturnRequestUserNotification.Build(userId, referenceNumber);
        await _notificationRepo.AddAsync(userNotification);

        // admin Notifications
        var admins = await _userRepo.GetAdminsAsync();
        foreach (var admin in admins)
        {
            var adminNotification = ReturnRequestAdminNotification.Build(admin.Id, referenceNumber);
            await _notificationRepo.AddAsync(adminNotification);
            // push notification
            var notificationDto = new NotificationDto
            {
                Type = adminNotification.Type,
                Url = adminNotification.Url,
                Title = adminNotification.Translations.Where(tr => tr.LanguageCode == "en").Select(tr => tr.Title).FirstOrDefault() ?? "",
                Message = adminNotification.Translations.Where(tr => tr.LanguageCode == "en").Select(tr => tr.Message).FirstOrDefault() ?? "",
                NotificationRelated = PushNotificationType.OrderReturn.ToString()
            };
            await _push.PushToUser(admin.Id, notificationDto);
        }
    }
    // send order emails
    public async Task SendOrderEmails(string userEmail, Return _return)
    {
        var templateService = new EmailTemplateService();
        //email to user
        if (!userEmail.IsNullOrEmpty())
        {
            var userEmailBody = await templateService.RenderAsync("User/NewReturn", _return);
            await _email.SendEmailAsync(userEmail, "Return Request Submitted", userEmailBody);
        }

        // email to admin
        var adminEmail = await _setting.GetValue("admin_email");
        if (!adminEmail.IsNullOrEmpty())
        {
            var adminEmailBody = await templateService.RenderAsync("Admin/NewReturn", _return);
            await _email.SendEmailAsync(adminEmail, "New Return Request Submitted", adminEmailBody);
        }
    }
}