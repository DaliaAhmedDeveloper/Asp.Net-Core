namespace OnlineStore.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;

public interface INotificationRepository : IGenericRepository<Notification>
{
    Task<IEnumerable<NotificationDto>> GetByUserIdAsync(int userId);
    Task<IEnumerable<Notification>> GetAllWithPaginationAsync(int userId, string searchTxt, int page, int pageSize);
    Task MarkAsReadAsync(int notificationId);
    Task<int> CountAllByIdAsync(int userId);
}
