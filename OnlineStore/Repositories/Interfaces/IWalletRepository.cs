
namespace OnlineStore.Repositories;

using System.Threading.Tasks;
using OnlineStore.Models;

public interface IWalletRepository
{
    Task<Wallet?> GetByUserIdAsync(int userId);
    Task<Wallet> AddAsync(Wallet wallet);
    Task UpdateAsync(Wallet wallet);
}
