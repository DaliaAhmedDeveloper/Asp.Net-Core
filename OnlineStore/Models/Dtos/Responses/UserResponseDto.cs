namespace OnlineStore.Models.Dtos.Responses;

using OnlineStore.Models;
using OnlineStore.Models.Enums;

public class UserResponseDto
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public UserType UserType { get; set; }
    public string? Token {get; set;}
    public string? RefreshToken {get; set;}

    public static UserResponseDto FromModel(User user)
    {
        var result = new UserResponseDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            UserType = user.UserType,
            Token = "",
            RefreshToken = ""
        };
        return result;
    }
}

