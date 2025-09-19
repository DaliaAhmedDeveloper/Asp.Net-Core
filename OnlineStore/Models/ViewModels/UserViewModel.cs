using OnlineStore.Models.Enums;

namespace OnlineStore.Models.ViewModels;

public class UserViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    //public string PasswordHash { get; set; } = string.Empty;
    public UserType UserType { get; set; } = UserType.User;// Enum , Admin, Customer
    public ProviderType? Provider { get; set; } = null;//  "Facebook" ,  "Google"
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int CountryId { get; set; }
    public int CityId { get; set; }
    public int StateId { get; set; }
    public string? CityName { get; set; }
    public string? StateName { get; set; }
    public string? CountryName { get; set; }
    public string? Password { get; set; }
}