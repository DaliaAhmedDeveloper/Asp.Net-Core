namespace OnlineStore.Models;

public class ProductVariant : SoftDeleteEntity
{

    public int Id { get; set; }
    public decimal Price { get; set; }
    public decimal? SalePrice { get; set; }
    public bool IsDefault { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!; // one product many variants
    public ICollection<VariantAttributeValue> VariantAttributeValues { get; set; } = new List<VariantAttributeValue>(); // variant has many attr value
    public Stock Stock { set; get; } = null!;

    private string _imageUrl = string.Empty;
    public string ImageUrl
    {
        get
        {
        string baseUrl = "/Product/image/";
           return  string.IsNullOrEmpty(_imageUrl) ? $"{baseUrl}default.png" : baseUrl + _imageUrl;
        }
        set
        {
            _imageUrl = value;
            // upload image 
        }
    }
}
