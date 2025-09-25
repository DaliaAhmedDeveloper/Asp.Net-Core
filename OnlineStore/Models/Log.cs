namespace OnlineStore.Models;
public class Log : BaseEntity
{
    public int Id { get; set; }

    // Exception-specific fields
    public string StackTrace { get; set; } = string.Empty;
    public string? InnerException { get; set; }

    // Translations (if you keep multilingual support)
    public ICollection<LogTranslation> Translations { get; set; } = new List<LogTranslation>();
}
