
namespace OnlineStore.Repositories;
using OnlineStore.Models;
public interface IPasswordResetRepository
{
    Task AddAsync(PasswordReset passwordReset);
    Task<PasswordReset?> GetByEmailAndTokenAsync(string email, string token);
    Task DeleteAsync(PasswordReset passwordReset);
}
