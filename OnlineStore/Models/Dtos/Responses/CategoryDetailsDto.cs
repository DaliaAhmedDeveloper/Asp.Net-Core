namespace OnlineStore.Models.Dtos.Responses;
public class CategoryDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string _imageUrl { get; set; } = string.Empty;
    public string ImageUrl
    {
        get
        {
            return "category/image/" + _imageUrl;
        }
        set
        {
            _imageUrl = value;
            // upload image 
        }
    }
    public ICollection<ProductSimpleDto> Products { get; set; } = new List<ProductSimpleDto>();
}