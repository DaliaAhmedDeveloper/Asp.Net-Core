namespace OnlineStore.Models;

public class RefreshToken : BaseEntity
{
    public int Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public int UserId { get; set; }
    public DateTime ExpiryDate { get; set; } = DateTime.UtcNow.AddDays(20);
    public bool IsRevoked { get; set; }
    public User User { get; set; } = null!;
}