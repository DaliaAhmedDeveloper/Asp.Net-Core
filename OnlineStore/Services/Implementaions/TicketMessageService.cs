namespace OnlineStore.Services;

using Microsoft.Extensions.Localization;
using OnlineStore.Helpers;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.Enums;
using OnlineStore.Models.ViewModels;
using OnlineStore.Notifications;
using OnlineStore.Repositories;
using OnlineStore.Services.BackgroundServices;

public class TicketMessageService : ITicketMessageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBackgroundTaskQueue _taskQueue;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IStringLocalizer<TicketMessageService> _localizer;

    public TicketMessageService(
        IUnitOfWork unitOfWork,
        IBackgroundTaskQueue taskQueue,
        IStringLocalizer<TicketMessageService> localizer,
        IServiceScopeFactory scopeFactory
        )
    {
        _unitOfWork = unitOfWork;
        _taskQueue = taskQueue;
        _localizer = localizer;
        _scopeFactory = scopeFactory;
    }

    /*=========== API ========================*/
    public async Task<TicketMessage> CreateTicketMessageAsync(CreateTicketMessageDto dto)
    {
         var ticketId = dto.TicketId ?? 0;
        var userId = dto.UserId ?? 0;

        var order = await _unitOfWork.SupportTicket.ExistsAsync(ticketId);
        if (!order)
            throw new NotFoundException(_localizer["TicketNotFound"]);
            
         var user = await _unitOfWork.User.ExistsAsync(userId);
        if (!user)
            throw new NotFoundException(_localizer["UserNotFound"]);
        /// model dto mapping 
        TicketMessage ticketMessage = new TicketMessage
        {
            Message = dto.Message,
            TicketId = ticketId,
            UserId = userId
        };
        var message = await _unitOfWork.TicketMessage.AddAsync(ticketMessage);

        // send notification and signalR to admins
        _taskQueue.Enqueue(async token =>
        {
            using var scope = _scopeFactory.CreateAsyncScope();
            var _notification = scope.ServiceProvider.GetRequiredService<INotificationService>();
            var _user = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            var _ticket = scope.ServiceProvider.GetRequiredService<ISupportTicketRepository>();
            var _push = scope.ServiceProvider.GetRequiredService<PushNotificationHelper>();
            if (message.UserId != null)
            {
                var user = await _user.GetByIdAsync(message.UserId ?? 0) ?? new User();
                var ticket = await _ticket.GetByIdAsync(message.TicketId) ?? new SupportTicket();

                // send  notification to admins + signalR
                var admins = await _user.GetAdminsAsync();
                foreach (var admin in admins)
                {
                    var adminNotification = TicketMessageAdminNotification.Build(admin.Id, ticket, user);
                    await _notification.Add(adminNotification);
                    // signalR
                    var notificationDto = new NotificationDto
                    {
                        Type = adminNotification.Type,
                        Url = adminNotification.Url,
                        Title = adminNotification.Translations.Where(tr => tr.LanguageCode == "en").Select(tr => tr.Title).FirstOrDefault() ?? "",
                        Message = adminNotification.Translations.Where(tr => tr.LanguageCode == "en").Select(tr => tr.Message).FirstOrDefault() ?? "",
                        NotificationRelated = PushNotificationType.NewMessage.ToString()
                    };
                    // push notification
                    await _push.PushToUser(admin.Id, notificationDto);
                }
            }
        });

        // send email  admins
        _taskQueue.Enqueue(async token =>
        {
            using var scope = _scopeFactory.CreateAsyncScope();
            var _email = scope.ServiceProvider.GetRequiredService<IEmailService>();
            var _user = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            var _ticket = scope.ServiceProvider.GetRequiredService<ISupportTicketRepository>();
            var _setting = scope.ServiceProvider.GetRequiredService<AppSettingHelper>();
            var templateService = new EmailTemplateService();
            if (message.UserId != null)
            {
                var user = await _user.GetByIdAsync(message.UserId ?? 0) ?? new User();
                var ticket = await _ticket.GetByIdAsync(message.TicketId) ?? new SupportTicket();
                message.User = user;
                message.Ticket = ticket;
                // send user email
                var userEmailBody = await templateService.RenderAsync("User/NewMessage", message);
                await _email.SendEmailAsync(user.Email, "Your Message Received", userEmailBody);

                await Task.Delay(30000);

                // send email to admin email
                var adminEmailBody = await templateService.RenderAsync("Admin/NewMessage", message);
                await _email.SendEmailAsync(await _setting.GetValue("admin_email"), "New Message Submitted", adminEmailBody);
            }
        });
        return message;
    }
    // list by ticket
    public async Task<IEnumerable<TicketMessage>> ListByTicket(int ticketId)
    {
        return await _unitOfWork.TicketMessage.GetByTicket(ticketId);
    }
    /*=========== WEB ========================*/
    // get
    public async Task<IEnumerable<TicketMessage>> GetAllForWeb()
    {
        return await _unitOfWork.TicketMessage.GetAllAsync();
    }
    // get all with pagination
    public async Task<PagedResult<TicketMessage>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _unitOfWork.TicketMessage.CountAllAsync();
        var categories = await _unitOfWork.TicketMessage.GetAllWithPaginationAsync(searchTxt, pageNumber, pageSize);
        var model = new PagedResult<TicketMessage>
        {
            Items = categories,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }
    // get details
    public async Task<TicketMessage?> GetForWeb(int id)
    {
        return await _unitOfWork.TicketMessage.GetByIdAsync(id);
    }
    public async Task<IEnumerable<TicketMessage>> GetByTicketForWeb(int id)
    {
        return await _unitOfWork.TicketMessage.GetByTicket(id);
    }
    // add new TicketMessage
    public async Task<TicketMessage> CreateForWeb(TicketMessageViewModel model)
    {
        var ticketMessageModel = new TicketMessage
        {
            UserId = model.UserId,
            Message = model.Message,
            TicketId = model.TicketId,
            IsFromStaff = model.IsFromStaff
        };

        var ticketMessage = await _unitOfWork.TicketMessage.AddAsync(ticketMessageModel);

        // send notification to user
        _taskQueue.Enqueue(async token =>
        {
            using var scope = _scopeFactory.CreateAsyncScope();
            var _notification = scope.ServiceProvider.GetRequiredService<INotificationService>();
            var _ticket = scope.ServiceProvider.GetRequiredService<ISupportTicketRepository>();
            var ticket = await _ticket.GetByIdAsync(ticketMessage.TicketId) ?? new SupportTicket();
            //send notification to user 
            var notification = TicketMessageUserNotification.Build(model.UserId, ticket );
            await _notification.Add(notification);
        });

        // send email to user
        _taskQueue.Enqueue(async token =>
        {
            using var scope = _scopeFactory.CreateAsyncScope();
            var _email = scope.ServiceProvider.GetRequiredService<IEmailService>();
            var _user = scope.ServiceProvider.GetRequiredService<IUserService>();
            var templateService = new EmailTemplateService();
            if (ticketMessage.UserId != null)
            {
                var user = await _user.Find(ticketMessage.UserId ?? 0);

                // send user email
                var userEmailBody = await templateService.RenderAsync("User/NewMessage", ticketMessage);
                await _email.SendEmailAsync(user.Email, "New Reply to Your Support Ticket", userEmailBody);
            }
        });
        return ticketMessage;
    }

    // update TicketMessage
    public async Task<TicketMessage> UpdateForWeb(TicketMessageViewModel model, TicketMessage ticketMessage)
    {
        await _unitOfWork.TicketMessage.UpdateAsync(ticketMessage);
        return ticketMessage;
    }

    // delete TicketMessage
    public async Task<bool> DeleteForWeb(int id)
    {
        var TicketMessage = await _unitOfWork.TicketMessage.GetByIdAsync(id);
        return await _unitOfWork.TicketMessage.DeleteAsync(id);
    }

}