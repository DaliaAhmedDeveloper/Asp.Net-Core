namespace OnlineStore.Models.Dtos.Requests;

using System.ComponentModel.DataAnnotations;
using OnlineStore.Models.Enums;
using OnlineStore.Resources;

public class CreateSupportTicketDto
{
    [Required (ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "UserIdRequired")] 
    public int? UserId { get; set; }

    [Required (ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "OrderIdRequired")]
    public int? OrderId { get; set; }

    [Required (ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "CategoryRequired")]
    public TicketCategory Category { get; set; }

    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "SubjectRequired")]
    //[StringLength(200, ErrorMessage = "Subject cannot exceed 200 characters")]
    [StringLength(200, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "SubjectLimit")]
    public string Subject { get; set; } = string.Empty;

    [Required (ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "DescriptionRequired")]
    public string Description { get; set; } = string.Empty;
}