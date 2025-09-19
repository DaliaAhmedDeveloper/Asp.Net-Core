namespace OnlineStore.Repositories;

using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
{
    private readonly ILanguageService _lang;
    public NotificationRepository(AppDbContext context, ILanguageService lang) : base(context)
    {
        _lang = lang;
    }
    public async Task<IEnumerable<NotificationDto>> GetByUserIdAsync(int userId)
    {
        var lang = _lang.GetCurrentLanguage();
        // return await _context.Notifications
        //     .Where(n => n.UserId == userId)
        //     .ToListAsync();
        return await _context.Notifications.Where(n => n.UserId == userId).Select(n => new NotificationDto
        {
            Id = n.Id,
            Type = n.Type,
            Url = n.Url,
            IsRead = n.IsRead,
            CreatedAt = n.CreatedAt,
            Title = n.Translations
                .Where(tr => tr.LanguageCode == lang)
                .Select(tr => tr.Title)
                .FirstOrDefault() ?? "",
            Message = n.Translations
                .Where(tr => tr.LanguageCode == lang)
                .Select(tr => tr.Title)
                .FirstOrDefault() ?? "",
        }).ToListAsync();
    }

    public async Task MarkAsReadAsync(int notificationId)
    {
        var notification = await _context.Notifications.FindAsync(notificationId);
        if (notification != null)
        {
            notification.IsRead = true;
            await _context.SaveChangesAsync();
        }
    }
    // 
    public async Task<int> CountAllByIdAsync(int userId)
    {
        return await _context.Notifications.Where(n => n.UserId == userId).CountAsync();
    }
     // get all with pagination 
    public async Task<IEnumerable<Notification>> GetAllWithPaginationAsync(
        int userId,
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var query = _context.Notifications.Where(n => n.UserId == userId).Include(n => n.Translations);
        if (!string.IsNullOrEmpty(searchTxt))
            return await query.Where(n => n.Translations.Any(t => t.Title.Contains(searchTxt))).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    } 
}
