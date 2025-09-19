namespace OnlineStore.Models.Dtos.Requests;

using System.ComponentModel.DataAnnotations;
using OnlineStore.Resources;

public class CreateTicketMessageDto
{
    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "TicketIdRequired")]
    public int? TicketId { get; set; }

    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "TicketMessageUserIdRequired")]
    public int? UserId { get; set; }

    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "TicketMessageRequired")]
    [StringLength(1000, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "TicketMessageLimit")]
    public string Message { get; set; } = string.Empty;

}