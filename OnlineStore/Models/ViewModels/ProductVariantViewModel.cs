using System.ComponentModel.DataAnnotations;
using OnlineStore.Models.Enums;

namespace OnlineStore.Models.ViewModels;

public class ProductVariantViewModel
{
    public int? Id { get; set; }
    public decimal Price { get; set; }
    public decimal? SalePrice { get; set; }

    // For upload (not saved directly in DB)
    public IFormFile? ImageFile { get; set; }

    // For display after upload (URL/path from DB)
    public string? ImageUrl { get; set; }
    public int AttributeId { get; set; }
    public int ValueId { get; set; }
    //stock
    public int WarehouseId { get; set; }
    public int TotalQuantity { get; set; }
    public int MinimumStockLevel { get; set; }
    public decimal UnitCost { get; set; } 
}