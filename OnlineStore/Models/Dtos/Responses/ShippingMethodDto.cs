
namespace OnlineStore.Models.Dtos.Responses;
public class ShippingMethodDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public string DeliveryTime { get; set; } = string.Empty;
}
