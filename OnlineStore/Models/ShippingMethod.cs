namespace OnlineStore.Models;

public class ShippingMethod :BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;  // Free, Flat, DHL, Aramex
    public decimal Cost { get; set; }
    public string DeliveryTime { get; set; } = string.Empty;

    public ICollection<Order> Orders { get; set; } = new List<Order>(); // has many orders
    public ICollection<ShippingMethodTranslation> Translations { get; set; } = new List<ShippingMethodTranslation>();
}