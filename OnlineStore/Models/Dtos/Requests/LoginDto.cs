namespace OnlineStore.Models.Dtos.Requests;
using System.ComponentModel.DataAnnotations;
using OnlineStore.Resources;

public class LoginDto
{
    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "LoginEmailRequired")]
    [EmailAddress(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "LoginEmailInvalid")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "LoginPasswordRequired")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
