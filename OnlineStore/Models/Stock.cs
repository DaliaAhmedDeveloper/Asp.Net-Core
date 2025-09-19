using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models;

public class Stock : BaseEntity
{
    public int Id { get; set; } 
    public int ProductVariantId { get; set; } 
    public int WarehouseId { get; set; }
    public int TotalQuantity { get; set; }
    public int ReservedQuantity { get; set; }
    public int AvailableQuantity  => TotalQuantity - ReservedQuantity;
    public int MinimumStockLevel { get; set; }
    public decimal UnitCost { get; set; }
    [Timestamp] // Concurrency token
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    public DateTime LastRestocked { get; set; }
    public DateTime LastStockCount { get; set; }
    
    // Navigation properties
    public ProductVariant ProductVariant { get; set; } = null!;
    public Warehouse Warehouse { get; set; } = null!;
    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
} 