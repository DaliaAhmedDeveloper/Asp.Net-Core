namespace OnlineStore.Repositories;

using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;

public class PasswordResetRepository : IPasswordResetRepository
{
    private readonly AppDbContext _context;

    public PasswordResetRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(PasswordReset passwordReset)
    {
        _context.PasswordResets.Add(passwordReset);
        await _context.SaveChangesAsync();
    }

    public async Task<PasswordReset?> GetByEmailAndTokenAsync(string email, string token)
    {
        return await _context.PasswordResets
            .FirstOrDefaultAsync(pr =>
                pr.Email == email &&
                pr.Token == token &&
                !pr.Used &&
                pr.ExpiresAt > DateTime.UtcNow);
    }

    // Delete token after use
    public async Task DeleteAsync(PasswordReset passwordReset)
    {
        _context.PasswordResets.Remove(passwordReset);
        await _context.SaveChangesAsync();
    }
}
