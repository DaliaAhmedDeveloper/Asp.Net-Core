namespace OnlineStore.Models;
public class WalletTransaction : BaseEntity
{
    public int Id { get; set; }
    public int WalletId { get; set; }
    public Wallet Wallet { get; set; } = null!;
    public decimal Amount { get; set; } 
    public string Description { get; set; } = string.Empty;
}

