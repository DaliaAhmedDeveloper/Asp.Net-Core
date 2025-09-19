using System.ComponentModel.DataAnnotations;
using OnlineStore.Resources;

namespace OnlineStore.Models.Dtos.Requests;

public class AddToCartDto
{
    [Required (ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "ProductIdRequired")]
    public int? ProductId { get; set; }

    [Required (ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "VariantIdRequired")]
    public int? VariantId { get; set; }
    public int Quantity { get; set; } = 1 ;
}