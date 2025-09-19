namespace OnlineStore.Repositories;
using OnlineStore.Models;
public interface IRefreshTokenRepository
{
    Task<RefreshToken> AddAsync(RefreshToken RefreshToken);
    Task<RefreshToken?> GetByUserAsync(int RefreshTokenId);
    Task<bool> UpdateAsync(RefreshToken RefreshToken);
    Task<RefreshToken?> GetWithUser(string refreshToken);
    Task<RefreshToken?> GetByUserIdAsync(int userId);
    Task<bool> DeleteAsync(int RefreshTokenId);
}