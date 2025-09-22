namespace OnlineStore.Models;
using OnlineStore.Models.Enums;
public class Product : BaseEntity
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public ProductType Type { get; set; } = ProductType.Simple;
    public decimal Price { get; set; }
    public decimal? SalePrice { get; set; }
    //public bool IsActive { get; set; } = true;
    private string _imageUrl = string.Empty;
    public string ImageUrl
    {
        get
        {
        string baseUrl = "/Product/image/";
           return string.IsNullOrEmpty(_imageUrl) ? $"{baseUrl}default.png" : baseUrl + _imageUrl;
        }
        set
        {
            _imageUrl = value;
            // upload image 
        }
    }
    // productCategory many to many relationship
    public ICollection<ProductTranslation> Translations { get; set; } = new List<ProductTranslation>();
    public ICollection<Category> Categories { get; set; } = new List<Category>(); // belongs to many categoris
    public ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>(); // has many variant
    public ICollection<Tag> Tags { get; set; } = new List<Tag>(); // has many product tags
    public ICollection<Review> Reviews { get; set; } = new List<Review>(); // has many reviews
}