namespace OnlineStore.Models;
public class ReviewAttachment
{
    public int Id { get; set; }

    public int ReviewId { get; set; }
    public Review Review { get; set; } = null!;
    public string FileName { get; set; } = string.Empty; 
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}
