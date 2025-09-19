using OnlineStore.Models.Enums;

namespace OnlineStore.Models.ViewModels;

public class EditOrderViewModel
{
    public int OrderId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public string ReferenceNumber { get; set; } = string.Empty; 
}