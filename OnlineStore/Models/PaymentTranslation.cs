namespace OnlineStore.Models;

public class PaymentTranslation
{
    public int Id { get; set; }
    public string Method { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;
    public int PaymentId { get; set; }
    public Payment Payment { get; set; } = null!;

}
