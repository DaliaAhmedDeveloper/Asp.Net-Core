using System.ComponentModel.DataAnnotations;
using OnlineStore.Resources;

namespace OnlineStore.Models.Dtos.Requests;
public class CartItemDto
{
    [Required (ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "ProductIdRequired")]
    public int? ProductId { get; set; }

    [Required (ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "VariantIdRequired")]
    public int? VariantId { get; set; }

    [Required (ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "QuantityRequired")]
    public int Quantity { get; set; } = 1;

    [Required (ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "CartIdRequired")]
    public int CartId { get; set; }
}