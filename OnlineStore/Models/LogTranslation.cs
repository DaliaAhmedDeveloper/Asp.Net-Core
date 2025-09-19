namespace OnlineStore.Models;

public class LogTranslation
{
    public int Id { get; set; }
    public string ExceptionTitle { get; set; } = string.Empty;
    public string ExceptionMessage { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;
    public int LogId { get; set; }
    public Log Log { get; set; } = null!;

}