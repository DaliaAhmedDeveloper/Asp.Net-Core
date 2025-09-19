namespace OnlineStore.Models.Dtos.Responses;

using System.Text.Json.Serialization;
using OnlineStore.Models.Enums;

public class UserDto
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public UserType UserType { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Token { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? RefreshToken { get; set; }
}