namespace OnlineStore.Models;

public class Payment : SoftDeleteEntity
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public ICollection<PaymentTranslation> Translations = new List<PaymentTranslation>();
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!; // belongs to one order
}
