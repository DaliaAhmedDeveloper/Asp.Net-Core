namespace OnlineStore.Repositories;

using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class WalletTransactionRepository : IWalletTransactionRepository
{
    private readonly AppDbContext _context;

    public WalletTransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WalletTransaction>> GetByWalletIdAsync(int walletId)
    {
        return await _context.WalletTransactions
            .Where(t => t.Id == walletId)
            .ToListAsync();
    }

    public async Task<WalletTransaction?> GetByIdAsync(int id)
    {
        return await _context.WalletTransactions.FindAsync(id);
    }

    public async Task<WalletTransaction> AddAsync(WalletTransaction transaction)
    {
        _context.WalletTransactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }
}
