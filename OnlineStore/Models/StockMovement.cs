using OnlineStore.Models.Enums;

namespace OnlineStore.Models;

public class StockMovement : BaseEntity
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public int Quantity { get; set; }
    public StockMovementType Type { get; set; }
    public string Reference { get; set; } = string.Empty; // Order ID, Purchase ID, etc.
    public string Notes { get; set; } = string.Empty;
    public int? UserId { get; set; } // Who made the change
    public decimal? UnitCost { get; set; } // Cost at time of movement

    // Navigation properties
    public Stock Stock { get; set; } = null!;
    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
} 