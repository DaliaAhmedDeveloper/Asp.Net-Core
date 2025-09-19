namespace OnlineStore.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineStore.Models;

public interface IWalletTransactionRepository
{
    Task<IEnumerable<WalletTransaction>> GetByWalletIdAsync(int userId);
    Task<WalletTransaction?> GetByIdAsync(int id);
    Task<WalletTransaction> AddAsync(WalletTransaction transaction);
}
