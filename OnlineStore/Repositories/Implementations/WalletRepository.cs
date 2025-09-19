namespace OnlineStore.Repositories;

using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using System.Threading.Tasks;

public class WalletRepository : IWalletRepository
{
    private readonly AppDbContext _context;

    public WalletRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Wallet?> GetByUserIdAsync(int userId)
    {
        return await _context.Wallet.Include(w => w.Transactions).FirstOrDefaultAsync(w => w.UserId == userId);
    }

    public async Task<Wallet> AddAsync(Wallet wallet)
    {
        _context.Wallet.Add(wallet);
        await _context.SaveChangesAsync();
        return wallet;
    }

    public async Task UpdateAsync(Wallet wallet)
    {
        _context.Wallet.Update(wallet);
        await _context.SaveChangesAsync();
    }
}
