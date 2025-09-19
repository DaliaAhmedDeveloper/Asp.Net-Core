namespace OnlineStore.Models.Dtos.Responses;

public class WishlistDto
{
    public int Id { get; set; }
    public ProductSimpleDto product { get; set; } = null!;
}
