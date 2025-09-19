namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public class WalletTransactionService : IWalletTransactionService
{
    private readonly IWalletTransactionRepository _transactionRepo;

    public WalletTransactionService(IWalletTransactionRepository transactionRepo)
    {
        _transactionRepo = transactionRepo;
    }

    public Task<IEnumerable<WalletTransaction>> ListByWalletId(int walletId)
    {
        return _transactionRepo.GetByWalletIdAsync(walletId);
    }

    public Task<WalletTransaction?> FindById(int id)
    {
        return _transactionRepo.GetByIdAsync(id);
    }

    public Task<WalletTransaction> Add(WalletTransaction transaction)
    {
        return _transactionRepo.AddAsync(transaction);
    }
}
