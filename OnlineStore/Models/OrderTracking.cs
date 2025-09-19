namespace OnlineStore.Models;
using OnlineStore.Models.Enums;
public class OrderTracking
{
    public int Id { get; set; }              // Primary key
    public int OrderId { get; set; }         // FK to Orders table
    public OrderStatus Status { get; set; } = OrderStatus.Pending; // Enum
    public string TrackingNumber { get; set; } = string.Empty; // Shipping company tracking number
    public string TrackingUrl { get; set; } = string.Empty;  // URL to check tracking on shipping website
    public string DriverName { get; set; } = string.Empty;   // Optional, if out for delivery
    public string DriverPhone { get; set; } = string.Empty;  // Optional, contact for delivery
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public Order Order { get; set; } = null!;
}
