namespace OnlineStore.Models;

using OnlineStore.Models.Enums;
public class UserPoint
{
    public int Id { get; set; }              // Primary key
    public int UserId { get; set; }          // FK to Users table
    public int Points { get; set; }          // Points earned or redeemed
    public PointType Type { get; set; }    // Reason for points (enum)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiryAt { get; set; } = DateTime.UtcNow.AddDays(5);
    public bool Expired { get; set; }
    public User User { get; set; } = null!;
}
