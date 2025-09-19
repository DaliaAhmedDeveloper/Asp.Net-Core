namespace OnlineStore.Models.Dtos.Responses;

using OnlineStore.Models.Enums;

public class ReturnDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public string ReferenceNumber { get; set; } = string.Empty;
    public string? Reason { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime ReturnDate { get; set; }
    public string RefundType { get; set; } = string.Empty;
    public List<ReturnItemDto> ReturnItems { get; set; } = new();
    public ReturnTrackingDto? ReturnTracking { get; set; }
}

public class ReturnItemDto
{
    public int Id { get; set; }
    public int OrderItemId { get; set; }
    public string ProductName { get; set; } = string.Empty; // من OrderItem
    public string? Reason { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }

    public List<ReturnAttachmentDto> Attachments { get; set; } = new();
}

public class ReturnAttachmentDto
{
    public int Id { get; set; }
    public string FileUrl { get; set; } = string.Empty;
}
public class ReturnTrackingDto
{
    public int Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public string TrackingNumber { get; set; } = string.Empty;
    public string TrackingUrl { get; set; } = string.Empty;
    public string DriverName { get; set; } = string.Empty;
    public string DriverPhone { get; set; } = string.Empty;
}
