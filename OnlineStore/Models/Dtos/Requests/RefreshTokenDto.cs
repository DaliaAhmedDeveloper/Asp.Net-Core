using System.ComponentModel.DataAnnotations;
namespace OnlineStore.Models.Dtos.Requests;

public class RefreshTokenDto
{
    [Required]
    public string RefreshTokenString { get; set; } = string.Empty;
}