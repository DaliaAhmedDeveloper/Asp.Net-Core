namespace OnlineStore.Models.ViewModels;
public class LogViewModel
{ 
    public int? Id { get; set; }
    public string StackTrace { get; set; } = string.Empty;
    public string? InnerException { get; set; }
    public string ExceptionTypeEn { get; set; } = string.Empty;
    public string ExceptionMessageEn { get; set; } = string.Empty;
    public string ExceptionTypeAr { get; set; } = string.Empty;
    public string ExceptionMessageAr { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<LogTranslation> Translations { get; set; } = new List<LogTranslation>();
}