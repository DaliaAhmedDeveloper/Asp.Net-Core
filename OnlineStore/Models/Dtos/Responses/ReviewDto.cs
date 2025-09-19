namespace OnlineStore.Models.Dtos.Responses;

public class ReviewDto
{
    public int Id { get; set; }
    public int Rating { get; set; }           // 1 to 5
    public string Comment { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public bool Accepted { get; set; }
    public string ProductImage { get; set; } = string.Empty;
    public List<ReviewAttachmentDto> Attachments { get; set; } = new List<ReviewAttachmentDto>();
    public UserDto User { get; set; } = null!;
}

public class ReviewAttachmentDto
{
    public string ImageUrl { get; set; } = string.Empty;
}
