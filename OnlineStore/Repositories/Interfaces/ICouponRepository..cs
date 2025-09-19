namespace OnlineStore.Repositories;
using OnlineStore.Models;
public interface ICouponRepository : IGenericRepository<Coupon>
{
    Task<CouponUser> AddUserCopounUsageAsync(CouponUser coupon);
    Task<bool> UpdateUserCopounUsageAsync(int userId, int couponId, int count);
    Task<CouponUser?> GetUserCopounUsageAsync(int userId, int couponId);
    Task<IEnumerable<Coupon>> GetAllWithPaginationAsync(string searchTxt , int page , int pageSize);
}