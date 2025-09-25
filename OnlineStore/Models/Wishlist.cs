namespace OnlineStore.Models;
public class Wishlist : BaseEntity
{
    public int Id { get; set; }

    // Foreign key to User
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    // Foreign key to Product
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
