namespace OnlineStore.Services;

using Microsoft.Extensions.Localization;
using OnlineStore.Models;
using OnlineStore.Repositories;
using System.Threading.Tasks;

public class WalletService : IWalletService
{
    private readonly IWalletRepository _walletRepo;
    private readonly IStringLocalizer<WalletService> _localizer;

    public WalletService(IWalletRepository walletRepo, IStringLocalizer<WalletService> localizer)
    {
        _walletRepo = walletRepo;
        _localizer = localizer;
    }

    public Task<Wallet?> FindByUserId(int userId)
    {
        return _walletRepo.GetByUserIdAsync(userId);
    }

    public Task<Wallet> Add(Wallet wallet)
    {
        return _walletRepo.AddAsync(wallet);
    }

    public Task Update(Wallet wallet)
    {
        return _walletRepo.UpdateAsync(wallet);
    }

     /*
    Wallet Check
    Apply Calculations On Final Price
    Return Updated Final Price
    */
    public decimal CheckWallet(decimal walletAmountUsed, decimal finalPrice, Wallet userWallet)
    {
        if (walletAmountUsed > 0)
        {

            if (walletAmountUsed > userWallet.Balance)
                throw new ResponseErrorException(_localizer["NotEnoughWalletBalance"]);

            finalPrice -= walletAmountUsed;
        }
        return finalPrice;
    }
    
}
