namespace OnlineStore.Models;
public class ReturnAttachment
{
    public int Id { get; set; }

    public int ReturnItemId { get; set; } // FK to ReturnItem
    public ReturnItem ReturnItem { get; set; } = null!;
    public string FileName { get; set; } = string.Empty; // Original file name

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}
