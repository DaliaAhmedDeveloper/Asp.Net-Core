namespace OnlineStore.Models.Dtos.Responses;

public class ProductDto
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public decimal? SalePrice { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Brand { get; set; }
    private string _imageUrl { get; set; } = string.Empty;
    public string ImageUrl
    {
        get
        {
            return "/Product/image/" + _imageUrl;
        }
        set
        {
            _imageUrl = value;
            // upload image 
        }
    }
    public List<CategoryDto> Categories { get; set; } = new();
    public List<TagDto> Tags { get; set; } = new();
    public List<ProductVariantDto> ProductVariants { get; set; } = new();
    
}