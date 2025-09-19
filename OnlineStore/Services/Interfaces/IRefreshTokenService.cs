namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
public interface IRefreshTokenService
{
    Task<UserResponseDto?> Refresh(string refreshTokenString);
    Task<RefreshToken?> GetByUserId(int userId);
    Task<bool> Delete(RefreshToken refreshToken);
}