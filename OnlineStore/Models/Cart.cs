namespace OnlineStore.Models;
public class Cart : BaseEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    //public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>(); // has many items 
}
