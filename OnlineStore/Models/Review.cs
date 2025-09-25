namespace OnlineStore.Models;

public class Review : BaseEntity
{
    public int Id { get; set; }
    public int Rating { get; set; } // 1 to 5
    public string Comment { get; set; } = string.Empty;
    public bool Accepted { get; set; }
    public int? UserId { get; set; }
    public User User { get; set; } = null!; // reiew belongs to one user and user has many reviews
    public int? OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!; // reiew belongs to one product and product has many reviews
    public ICollection<ReviewAttachment> Attachments { get; set; } = new List<ReviewAttachment>();
}