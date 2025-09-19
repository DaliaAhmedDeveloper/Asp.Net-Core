namespace OnlineStore.Models.Dtos.Requests;
using System.ComponentModel.DataAnnotations;
using OnlineStore.Resources;
public class ChangePasswordDto
{
    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "PasswordRequired")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&#]{8,}$",
        ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "PasswordComplexity")]
    public string Password { get; set; } = string.Empty;

    [Compare("Password", ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "PasswordsDoNotMatch")]
    public string ConfirmPassword { get; set; } = string.Empty;
}