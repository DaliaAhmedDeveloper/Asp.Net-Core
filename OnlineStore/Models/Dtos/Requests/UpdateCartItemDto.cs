using System.ComponentModel.DataAnnotations;
using OnlineStore.Resources;

namespace OnlineStore.Models.Dtos.Requests;

public class UpdateCartDto
{
    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "CartItemIdRequired")]
    public int CartItemId { get; set; }

    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "QuantityRequired")]
    public int Quantity { get; set; } = 1 ;
}