using System.ComponentModel.DataAnnotations;
using OnlineStore.Models.Enums;

namespace OnlineStore.Models.ViewModels;

public class UpdateUserViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Full Name Is Required")]
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$",
    ErrorMessage = "Password must be at least 8 characters and include uppercase, lowercase, number, and special character.")]
    public string? PasswordHash { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    [Required(ErrorMessage = "Country Is Required")]
    public int CountryId { get; set; }

    [Required(ErrorMessage = "City Is Required")]
    public int CityId { get; set; }

    [Required(ErrorMessage = "State Is Required")]
    public int StateId { get; set; }
}