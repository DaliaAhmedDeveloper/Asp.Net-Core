using System.Transactions;
using OnlineStore.Models.Enums;

namespace OnlineStore.Models;

public class NotificationTranslation
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;
    public int NotificationId { get; set; }
    public Notification Notification { get; set; } = null!;
}
