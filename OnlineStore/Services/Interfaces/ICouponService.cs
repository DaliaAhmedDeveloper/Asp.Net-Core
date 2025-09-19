namespace OnlineStore.Services;
using OnlineStore.Models.ViewModels;
using OnlineStore.Models;

public interface ICouponService
{
    //api
    Task<(decimal finalPrice, decimal couponDiscountValue)> CheckCoupon(
        int couponId,
        int userId,
        decimal totalPriceAfterSale,
        decimal finalPrice
        );
    // web
    Task<Coupon> CreateForWeb(CouponViewModel model);
    Task<Coupon?> GetForWeb(int id);
    Task<PagedResult<Coupon>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber, int pageSize);
    Task<IEnumerable<Coupon>> GetAllForWeb();
    Task<Coupon> UpdateForWeb(CouponViewModel model, Coupon Coupon);
    Task<bool> DeleteForWeb(int id);
} 