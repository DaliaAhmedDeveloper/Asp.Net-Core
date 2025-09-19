namespace OnlineStore.Services;

using OnlineStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IWalletTransactionService
{
    Task<IEnumerable<WalletTransaction>> ListByWalletId(int walletId);
    Task<WalletTransaction?> FindById(int id);
    Task<WalletTransaction> Add(WalletTransaction transaction);
}
