using OnlineStore.Models.Enums;

namespace OnlineStore.Models;

public class Notification
{
    public int Id { get; set; }
    public NotificationType Type { get; set; } // Info, Warning, Error, etc.
    public string Url { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public User User { get; set; } = null!; // belongs to one user
    public ICollection<NotificationTranslation> Translations = new List<NotificationTranslation >();
}
