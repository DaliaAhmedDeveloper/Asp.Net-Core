using Microsoft.EntityFrameworkCore;
using OnlineStore.Helpers;
using OnlineStore.Models;

namespace OnlineStore.Repositories;

public class UserPointRepository : IUserPointRepository
{
    private readonly AppDbContext _context;
    public UserPointRepository(AppDbContext context)
    {
        _context = context;
    }

    // get all by user
    public async Task<IEnumerable<UserPoint>> GetAllByUserAsync(int id)
    {
        return await _context.UserPoints.Where(a => a.UserId == id).ToListAsync();
    }
    // add new UserPoint 
    public async Task<UserPoint> AddAsync(UserPoint UserPoint)
    {
        _context.UserPoints.Add(UserPoint);
        await _context.SaveChangesAsync();
        return UserPoint;
    }
    // get UserPoint details
    public async Task<UserPoint?> GetByIdAsync(int id)
    {
        return await _context.UserPoints.FindAsync(id);
    }

    // check if user get today points 
    public async Task<bool> CheckTodaysPoints()
    {
        var today = DateTime.UtcNow.Date; // Midnight today

        return await _context.UserPoints
            .AnyAsync(up => up.CreatedAt.Date == today);
    }
}