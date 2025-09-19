namespace OnlineStore.Services;
using OnlineStore.Models.Dtos.Requests;
public interface IPasswordResetService
{
    Task<bool> RequestPasswordResetAsync(string email);
    Task<bool> ResetPasswordAsync(PasswordResetDto dto);
}
