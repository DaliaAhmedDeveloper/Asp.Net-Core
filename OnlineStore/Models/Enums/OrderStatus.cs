namespace OnlineStore.Models.Enums;

public enum OrderStatus
{
    Pending = 0,     // Order placed but not yet processed
    Approved = 1,    // Order approved for processing
    Rejected = 2,    // Order rejected (e.g., payment failure)
    Processing = 3,  // Order is being prepared/shipped
    Shipped = 4,     // Order has been shipped
    Delivered = 5,   // Order delivered to customer
    Cancelled = 6,    // Order cancelled before completion
    OutForDeleviery = 7
}
