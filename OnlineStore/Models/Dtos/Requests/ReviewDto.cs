namespace OnlineStore.Models.Dtos.Requests;

using System.ComponentModel.DataAnnotations;
using OnlineStore.Resources;

public class ReviewDto
{
    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "ProductIdRequired")]
    public int? ProductId { get; set; }

    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "OrderIdRequired")]
    public int? OrderId { get; set; }

    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "OrderItemIdRequired")]
    public int? OrderItemId { get; set; }

    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "CommentRequired")]
    public string Comment { get; set; } = string.Empty;

    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RatingRequired")]
    public int Rating { get; set; }

}
