namespace OnlineStore.Models.Dtos.Responses;

public class ProductV2Dto
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public decimal? SalePrice { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? _imageUrl { get; set; }
    public string? Brand { get; set; }
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
    public ICollection<CategoryV2Dto> Categories { get; set; } = new List<CategoryV2Dto>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<TagV2Dto> Tags { get; set; } = new List<TagV2Dto>();
    public ICollection<ProductTranslation> Translations { get; set; } = new List<ProductTranslation>();
    public ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}

public class TagV2Dto
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public string? Title { get; set; }
    public ICollection<TagTranslation> Translations { get; set; } = new List<TagTranslation>();
}
public class CategoryV2Dto
{
    public int Id { get; set; }
    public string Slug { get; set; } = null!;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public ICollection<CategoryTranslation> Translations { get; set; } = new List<CategoryTranslation>();
}