using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;

namespace OnlineStore.Repositories;

public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
{
    public CouponRepository(AppDbContext context) : base(context)
    {
    }

    // check copoun user usage
    public async Task<CouponUser?> GetUserCopounUsageAsync(int userId, int couponId)
    {
        return await _context.couponUser
            .Where(cu => cu.CouponId == couponId)
            .Where(cu => cu.UserId == userId)
            .FirstOrDefaultAsync();
    }

    // Update copoun user usage
    public async Task<bool> UpdateUserCopounUsageAsync(int userId, int couponId, int count)
    {
        var record = await _context.couponUser
            .Where(cu => cu.CouponId == couponId)
            .Where(cu => cu.UserId == userId)
            .FirstAsync();
        record.UsageCount = count;
        return await _context.SaveChangesAsync() > 0;

    }

    // add copoun user usage
    public async Task<CouponUser> AddUserCopounUsageAsync(CouponUser coupon)
    {
        var record = await _context.couponUser.AddAsync(coupon);
        await _context.SaveChangesAsync();
        return coupon;
    }
    // get all with pagination 
    public  async Task<IEnumerable<Coupon>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {
        if (!string.IsNullOrEmpty(searchTxt))
            return await _context.Coupons.Include(c => c.Translations).Where(c => c.Code.Contains(searchTxt) || c.Translations.Any(ct => ct.Description.Contains(searchTxt))).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await _context.Coupons.Include(c => c.Translations).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }
}
