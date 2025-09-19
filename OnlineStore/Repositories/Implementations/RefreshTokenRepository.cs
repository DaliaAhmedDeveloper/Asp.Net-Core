using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;

namespace OnlineStore.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _context;
    public RefreshTokenRepository(AppDbContext context)
    {
        _context = context;
    }
    // get by user
    public async Task<RefreshToken?> GetByUserAsync(int userId)
    {
        return await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.UserId == userId);
    }

    // add new RefreshToken 
    public async Task<RefreshToken> AddAsync(RefreshToken RefreshToken)
    {
        _context.RefreshTokens.Add(RefreshToken);
        await _context.SaveChangesAsync();
        return RefreshToken;
    }
    // update RefreshToken 
    public async Task<bool> UpdateAsync(RefreshToken RefreshToken)
    {
        _context.RefreshTokens.Update(RefreshToken);
        return await _context.SaveChangesAsync() > 0; // returns true if at least one row affected
    }
    // get with user
    public async Task<RefreshToken?> GetWithUser(string refreshToken)
    {
        return await _context.RefreshTokens.Include(rt => rt.User).FirstOrDefaultAsync(rt => rt.Token == refreshToken);
    }
    // get by user id 
    public async Task<RefreshToken?> GetByUserIdAsync(int userId)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == userId);
    }
    // remove RefreshToken
    public async Task<bool> DeleteAsync(int RefreshTokenId)
    {
        var RefreshToken = await _context.RefreshTokens.FindAsync(RefreshTokenId);
        if (RefreshToken == null)
            return false;

        _context.RefreshTokens.Remove(RefreshToken);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}