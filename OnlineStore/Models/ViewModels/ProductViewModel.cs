using System.ComponentModel.DataAnnotations;
using OnlineStore.Models.Enums;

namespace OnlineStore.Models.ViewModels;

public class ProductViewModel
{
    public int? Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public ProductType Type { get; set; } = ProductType.Simple;
    public decimal Price { get; set; }
    public decimal? SalePrice { get; set; }
    public string NameEn { get; set; } = string.Empty;
    public string DescriptionEn { get; set; } = string.Empty;
    public string BrandEn { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    public string DescriptionAr { get; set; } = string.Empty;
    public string BrandAr { get; set; } = string.Empty;

    // For upload (not saved directly in DB)
    public IFormFile? ImageFile { get; set; }

    // For display after upload (URL/path from DB)
    public string? ImageUrl { get; set; }

    public int AttributeId { get; set; }
    public int ValueId { get; set; }

     public decimal? PriceBasedAttribute { get; set; }
    public decimal? SalePriceBasedAttribute { get; set; }
    public List<int> SelectedCategoryIds { get; set; } = new List<int>();
    public List<int> SelectedTagIds { get; set; } = new List<int>();
    public ICollection<ProductTranslation> Translations { get; set; } = new List<ProductTranslation>();

    //stock
    public int WarehouseId { get; set; }
    public int TotalQuantity { get; set; }
    public int MinimumStockLevel { get; set; }
    public decimal UnitCost { get; set; } 
}