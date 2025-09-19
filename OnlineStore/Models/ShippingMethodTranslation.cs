namespace OnlineStore.Models;

public class ShippingMethodTranslation
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;  // Free, Flat, DHL, Aramex
    public int ShippingMethodId { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
    public ShippingMethod ShippingMethod { get; set; } = null!;
}