namespace OnlineStore.Models.Dtos.Requests;
using System.ComponentModel.DataAnnotations;
using OnlineStore.Resources;
public class CreateUserDto
{
    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "FullNameRequired")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "EmailRequired")]
    [EmailAddress(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "EmailInvalid")]
    [UniqueEmail(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "EmailUnique")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "PhoneRequired")]
    [RegularExpression(@"^\+?[0-9]{7,15}$", ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "PhoneInvalid")]
    [UniquePhone(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "PhoneUnique")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "PasswordRequired")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&#]{8,}$",
        ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "PasswordComplexity")]
    public string Password { get; set; } = string.Empty;

    [Compare("Password", ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "PasswordsDoNotMatch")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "CountryRequired")]
    public int? CountryId { get; set; }

    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "CityRequired")]
    public int? CityId { get; set; }

    [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "StateRequired")]
    public int? StateId { get; set; }
}