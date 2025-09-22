namespace OnlineStore.Models.Dtos.Responses;

public class ProductSimpleDto
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public decimal? SalePrice { get; set; }
    public string? Title { get; set; }
    public string? _imageUrl { get; set; }
    public ICollection<ProductTranslation> Translations { get; set; } = new List<ProductTranslation>();
    public string ImageUrl
    {
        get
        {
            string baseUrl = "/product/image/";
            return string.IsNullOrEmpty(_imageUrl) ? $"{baseUrl}default.png" : baseUrl + _imageUrl;
        }
        set
        {
            _imageUrl = value;
            // upload image 
        }
    }
}