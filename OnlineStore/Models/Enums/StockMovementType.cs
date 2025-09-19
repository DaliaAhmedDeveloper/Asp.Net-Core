namespace OnlineStore.Models.Enums;

public enum StockMovementType
{
    Purchase = 0,        // Stock added from supplier
    Sale = 1,            // Stock reduced from sale
    Return = 2,          // Stock added from customer return
    Adjustment = 3,      // Manual stock adjustment
    Transfer = 4,        // Stock moved between warehouses
    Damage = 5,          // Stock reduced due to damage
    Expiry = 6,          // Stock reduced due to expiry
} 