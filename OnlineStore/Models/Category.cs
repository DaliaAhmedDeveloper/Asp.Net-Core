namespace OnlineStore.Models;

public class Category : BaseEntity
{
    public int Id { get; set; } // Automatically assigned by DB
    public string Slug { get; set; } = string.Empty;
    public int? ParentId { get; set; } // means can be null ,, will make it null field inside migration
    public bool IsDeal { get; set; }
    private string _imageUrl = string.Empty;
    public Category? Parent { get; set; }
    public ICollection<CategoryTranslation> Translations { get; set; } = new List<CategoryTranslation>();
    public ICollection<Category> Children { get; set; } = new List<Category>(); // Subcategories
    public ICollection<Product> Products { get; set; } = new List<Product>(); // CategoryProduct  Many-to-many relation

    public string ImageUrl
    {
        get
        {
            string baseUrl = "/category/image/";
            return string.IsNullOrEmpty(_imageUrl) ? $"{baseUrl}default.png" : baseUrl + _imageUrl;
        }
        set
        {
            _imageUrl = value;
            // upload image 
        }
    }
}
