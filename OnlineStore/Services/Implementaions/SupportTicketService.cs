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

public class SupportTicketService : ISupportTicketService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBackgroundTaskQueue _taskQueue;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly PushNotificationHelper _push;
    private readonly IStringLocalizer<SupportTicketService> _localizer;

    public SupportTicketService(
        IUnitOfWork unitOfWork,
        IBackgroundTaskQueue taskQueue,
        IServiceScopeFactory scopeFactory,
        IStringLocalizer<SupportTicketService> localizer,
        PushNotificationHelper push
        )
    {
        _unitOfWork = unitOfWork;
        _taskQueue = taskQueue;
        _localizer = localizer;
        _scopeFactory = scopeFactory;
        _push = push;
    }

    /*=========== API ========================*/

    public async Task<IEnumerable<SupportTicket>> ListByOrder(int orderId)
    {
        return await _unitOfWork.SupportTicket.GetByOrder(orderId);
    }
    public async Task<IEnumerable<SupportTicket>> ListByUser(int userId)
    {
        return await _unitOfWork.SupportTicket.GetByUser(userId);
    }
    // add new ticket
    public async Task<SupportTicket> CreateSupportTicketAsync(CreateSupportTicketDto dto)
    {
        var orderId = dto.OrderId ?? 0;
        var userId = dto.UserId ?? 0;

        var order = await _unitOfWork.Order.ExistsAsync(orderId);
        if (!order)
            throw new NotFoundException(_localizer["OrderNotFound"]);
            
         var user = await _unitOfWork.User.ExistsAsync(userId);
        if (!user)
            throw new NotFoundException(_localizer["UserNotFound"]);
    
        /// model dto mapping 
        SupportTicket supportTicket = new SupportTicket
        {
            Category = dto.Category,
            UserId = dto.UserId ?? 0,
            OrderId = dto.OrderId ?? 0,
            Subject = dto.Subject,
            Description = dto.Description,
            TicketNumber = $"TCK-{DateTime.UtcNow:yyyy}-{Guid.NewGuid().ToString("N").Substring(0, 8)}"
        };
        var ticket = await _unitOfWork.SupportTicket.AddAsync(supportTicket);

        // send notification and signalR to admins
        _taskQueue.Enqueue(async token =>
        {
            using var scope = _scopeFactory.CreateAsyncScope();
            var _notification = scope.ServiceProvider.GetRequiredService<INotificationService>();
            var _user = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            // send user email and notification
            var notification = TicketUserNotification.Build(ticket);
            await _notification.Add(notification);

            // send notification to admins + signalR
            var admins = await _user.GetAdminsAsync();
            var user = await _user.GetByIdAsync(ticket.UserId) ?? new User();
            foreach (var admin in admins)
            {
                var adminNotification = TicketAdminNotification.Build(admin.Id, ticket , user);
                await _notification.Add(adminNotification);
                // signalR
                var notificationDto = new NotificationDto
                {
                    Type = adminNotification.Type,
                    Url = adminNotification.Url,
                    Title = adminNotification.Translations.Where(tr => tr.LanguageCode == "en").Select(tr => tr.Title).FirstOrDefault() ?? "",
                    Message = adminNotification.Translations.Where(tr => tr.LanguageCode == "en").Select(tr => tr.Message).FirstOrDefault() ?? "",
                    NotificationRelated = PushNotificationType.NewTicket.ToString()
                };
                // push notification
                await _push.PushToUser(admin.Id, notificationDto);
            }
        });
        // send email admins
        _taskQueue.Enqueue(async token =>
        {
            using var scope = _scopeFactory.CreateAsyncScope();
            var _email = scope.ServiceProvider.GetRequiredService<IEmailService>();
            var _user = scope.ServiceProvider.GetRequiredService<IUserService>();
            var _order = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
            var _setting = scope.ServiceProvider.GetRequiredService<AppSettingHelper>();
            var templateService = new EmailTemplateService();
            var user = await _user.Find(ticket.UserId);

            // send user email
            var userEmailBody = await templateService.RenderAsync("User/NewTicket", ticket);
            await _email.SendEmailAsync(user.Email, "Support Ticket Received", userEmailBody);

            await Task.Delay(30000);
            // send admin email
            var order = await _order.GetByIdAsync(ticket.OrderId) ?? new Order();
            ticket.Order = order;
            ticket.User = user;
            var adminEmailBody = await templateService.RenderAsync("Admin/NewTicket", ticket);
            await _email.SendEmailAsync(await _setting.GetValue("admin_email"), "New Ticket Submitted", adminEmailBody);
        });
       
        return ticket;
    }

    /*=========== WEB ========================*/
    // get
    public async Task<IEnumerable<SupportTicket>> GetAllForWeb()
    {
        return await _unitOfWork.SupportTicket.GetAllAsync();
    }
    // get all with pagination
    public async Task<PagedResult<SupportTicket>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _unitOfWork.SupportTicket.CountAllAsync();
        var categories = await _unitOfWork.SupportTicket.GetAllWithPaginationAsync(searchTxt, pageNumber, pageSize);
        var model = new PagedResult<SupportTicket>
        {
            Items = categories,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }
    // get details
    public async Task<SupportTicket?> GetForWeb(int id)
    {
        return await _unitOfWork.SupportTicket.GetByIdWithRelationsAsync(id);
    }

    // add new SupportTicket
    public async Task<SupportTicket> CreateForWeb(SupportTicketViewModel model)
    {
        var supportTicket = new SupportTicket
        {
           
        };

        await _unitOfWork.SupportTicket.AddAsync(supportTicket);
        return supportTicket;
    }
    // update SupportTicket
    public async Task<SupportTicket> UpdateForWeb(SupportTicketViewModel model, SupportTicket SupportTicket)
    {
        await _unitOfWork.SupportTicket.UpdateAsync(SupportTicket);
        return SupportTicket;
    }
   
    // delete SupportTicket
    public async Task<bool> DeleteForWeb(int id)
    {
        var SupportTicket = await _unitOfWork.SupportTicket.GetByIdAsync(id);
        return await _unitOfWork.SupportTicket.DeleteAsync(id);
    }
   
} 