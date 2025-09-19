namespace OnlineStore.Models.Dtos.Responses;

public class CartDto
{
    public int Id { get; set; }
     public int UserId { get; set; }
    public ICollection<CartItemDto> Items { get; set; } = new List<CartItemDto>();
}
