namespace OnlineStore.Models;
public class PasswordReset
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    // Optional: Expiration date
    public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddMinutes(5);
    // Optional: track if used
    public bool Used { get; set; } = false;
}
