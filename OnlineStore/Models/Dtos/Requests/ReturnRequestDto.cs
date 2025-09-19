namespace OnlineStore.Models.Dtos.Requests;
using System.ComponentModel.DataAnnotations;
using OnlineStore.Resources;

public class ReturnRequestDto
{
    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "OrderIdRequired")]
    public int? OrderId { get; set; }
    public ICollection<ReturnRequestItemDto> Items { get; set; } = new List<ReturnRequestItemDto>();
}
public class ReturnRequestItemDto
{
    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "OrderItemIdRequired")]
    public int? OrderItemId { get; set; }
    public int Quantity { get; set; } = 1;

    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "ReasonRequired")]
    [MinLength(30, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "ReasonMinLength")]
    public string Reason { get; set; } = string.Empty;
}
