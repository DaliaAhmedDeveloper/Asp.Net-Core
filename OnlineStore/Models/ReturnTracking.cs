namespace OnlineStore.Models;
using OnlineStore.Models.Enums;
public class ReturnTracking
{
    public int Id { get; set; }             // Primary key
    public int ReturnId { get; set; }        // FK to Orders table
    public ReturnStatus Status { get; set; } = ReturnStatus.Pending;
    public string TrackingNumber { get; set; } = string.Empty;  // Return shipping number
    public string TrackingUrl { get; set; } = string.Empty;   // URL to track return
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public Return Return { get; set; } = null!;
}
