namespace OnlineStore.Services;

using OnlineStore.Models;
using System.Threading.Tasks;

public interface IWalletService
{
    Task<Wallet?> FindByUserId(int userId);
    Task<Wallet> Add(Wallet wallet);
    Task Update(Wallet wallet);
    decimal CheckWallet(decimal walletAmountUsed, decimal finalPrice, Wallet userWallet);
}
