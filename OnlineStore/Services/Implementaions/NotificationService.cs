namespace OnlineStore.Services;

using Microsoft.Extensions.Localization;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.ViewModels;
using OnlineStore.Repositories;
public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepo;
    private readonly IStringLocalizer<NotificationService> _localizer;
    public NotificationService(INotificationRepository notificationRepo, IStringLocalizer<NotificationService> localizer)
    {
        _notificationRepo = notificationRepo;
        _localizer = localizer;
    }
    //========================API===============//
    // add new Notification 
    public async Task<Notification> Add(Notification notification)
    {
        // add items to the Notification 
        return await _notificationRepo.AddAsync(notification);
    }
    // remove Notification
    public async Task<bool> Delete(int id)
    {
        bool status = await _notificationRepo.DeleteAsync(id);
        if (!status)
            throw new NotFoundException(string.Format(_localizer["NotificationNotFound"], id));
        return status;
    }
    // list all BY USER Notifications
    public async Task<IEnumerable<NotificationDto>> ListByUser(int id)
    {
        return await _notificationRepo.GetByUserIdAsync(id);
    }
    /*=========== WEB ========================*/
    // get
    public async Task<IEnumerable<Notification>> GetAllForWeb()
    {
        return await _notificationRepo.GetAllWithTranslationsAsync();
    }
   // get all with pagination
    public async Task<PagedResult<Notification>> GetAllWithPaginationForWeb(int userId ,string searchTxt, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _notificationRepo.CountAllByIdAsync(userId);
        var categories = await _notificationRepo.GetAllWithPaginationAsync(userId,searchTxt, pageNumber, pageSize);
        var model = new PagedResult<Notification>
        {
            Items = categories,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }
    // delete notification
    public async Task<bool> DeleteForWeb(int id)
    {
        return await _notificationRepo.DeleteAsync(id);
        
    }
   
}