using OnlineStore.Models.Enums;

namespace OnlineStore.Models.Dtos.Responses;

public class OrderListDto
{
    public int Id { get; set; }
    public string ReferenceNumber { get; set; } = string.Empty;
    public OrderStatus OrderStatus { get; set; }
    public int ItemCount;
    public List<ListOrderItemDto> Items { get; set; } = new List<ListOrderItemDto>();
    public decimal FinalAmount { get; set; }
}
public class ListOrderItemDto
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}