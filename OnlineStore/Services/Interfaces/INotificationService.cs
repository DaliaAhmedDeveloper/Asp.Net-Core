namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface INotificationService
{
    //api
    Task<Notification> Add(Notification notification);
    Task<bool> Delete(int id);
    Task<IEnumerable<NotificationDto>> ListByUser(int id);
    // web
    Task<IEnumerable<Notification>> GetAllForWeb();
    Task<PagedResult<Notification>> GetAllWithPaginationForWeb(int userId , string searchTxt , int pageNumber, int pageSize);
    Task<bool> DeleteForWeb(int id);
}
