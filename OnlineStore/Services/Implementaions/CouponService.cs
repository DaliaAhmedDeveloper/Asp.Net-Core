namespace OnlineStore.Services;

using Microsoft.Extensions.Localization;
using OnlineStore.Models;
using OnlineStore.Models.Enums;
using OnlineStore.Models.ViewModels;
using OnlineStore.Repositories;

public class CouponService : ICouponService
{
    private readonly ICouponRepository _couponRepo;
    private readonly IStringLocalizer<CouponService> _localizer;

    public CouponService(ICouponRepository couponRepo, IStringLocalizer<CouponService> localizer)
    {
        _couponRepo = couponRepo;
        _localizer = localizer;
    }

    /*=========== API ========================*/
    /*
    Check Coupon
    Apply calculations On Final Price
    Return Updated Final Price
    */
    public async Task<(decimal finalPrice , decimal couponDiscountValue)> CheckCoupon(int couponId, int userId, decimal totalPriceAfterSale, decimal finalPrice)
    {
        decimal couponDiscountValue = 0;
        var coupon = await _couponRepo.GetByIdAsync(couponId);

        // check user usage of coupon & update it
        var couponUsage = await _couponRepo.GetUserCopounUsageAsync(userId, couponId);
        if (couponUsage == null)
        {
            // create it 
            CouponUser couponUser = new CouponUser
            {
                UserId = userId,
                CouponId = couponId,
                UsageCount = 1
            };
            await _couponRepo.AddUserCopounUsageAsync(couponUser);
        }
        else
        {
            //check usage and  update it 
            if (couponUsage.UsageCount >= coupon?.MaxUsagePerUser)
                throw new ResponseErrorException(_localizer["CouponLimitExceeded"]);

            couponUsage.UsageCount += 1;
            await _couponRepo.UpdateUserCopounUsageAsync(userId, couponId, couponUsage.UsageCount);
        }

        if (coupon == null || !coupon.IsActive)
            throw new NotFoundException(_localizer["CouponNotValid"]);

        if (totalPriceAfterSale < coupon.MinimumOrderAmount)
            throw new ResponseErrorException(string.Format(_localizer["CartTotalTooLow"], coupon.MinimumOrderAmount));

        if (coupon.DiscountType == DiscountType.Fixed)
        {
            if (coupon.DiscountValue != null && coupon.DiscountValue > 0)
            {
                couponDiscountValue = (decimal)coupon.DiscountValue;
                finalPrice -= couponDiscountValue;
            }
        }
        else if (coupon.DiscountType == DiscountType.Percentage)
        {
            if (coupon.DiscountPrecentage != null && coupon.DiscountPrecentage > 0)
            {
                decimal discountedValueByPrecentage = (decimal)(totalPriceAfterSale * (coupon.DiscountPrecentage / 100));
                if (discountedValueByPrecentage > coupon.MaxDiscountAmount)
                {
                    discountedValueByPrecentage = coupon.MaxDiscountAmount;
                }
                couponDiscountValue = discountedValueByPrecentage;
                finalPrice -= discountedValueByPrecentage;
            }
        }
        return (finalPrice , couponDiscountValue);
    }

    /*=========== WEB ========================*/
    // get
    public async Task<IEnumerable<Coupon>> GetAllForWeb()
    {
        return await _couponRepo.GetAllAsync();
    }
    // get all with pagination
    public async Task<PagedResult<Coupon>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _couponRepo.CountAllAsync();
        var categories = await _couponRepo.GetAllWithPaginationAsync(searchTxt, pageNumber, pageSize);
        var model = new PagedResult<Coupon>
        {
            Items = categories,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }
    // get details
    public async Task<Coupon?> GetForWeb(int id)
    {
        return await _couponRepo.GetWithTranslationsAsync(id);
    }
    // add new coupon
    public async Task<Coupon> CreateForWeb(CouponViewModel model)
    {
        var coupon = new Coupon
        {
            Translations = new List<CouponTranslation>
            {
                new CouponTranslation { LanguageCode = "en",Description  = model.DescriptionEn },
                new CouponTranslation { LanguageCode = "ar", Description = model.DescriptionAr }
            }
        };

        await _couponRepo.AddAsync(coupon);
        return coupon;
    }
    // update coupon
    public async Task<Coupon> UpdateForWeb(CouponViewModel model, Coupon coupon)
    {
        foreach (var translation in coupon.Translations)
        {
            if (translation.LanguageCode == "en")
            {
                translation.Description = model.DescriptionEn;
            }
            else if (translation.LanguageCode == "ar")
            {
                translation.Description = model.DescriptionAr;
            }
        }

        await _couponRepo.UpdateAsync(coupon);
        return coupon;
    }
   
    // delete coupon
    public async Task<bool> DeleteForWeb(int id)
    {
        var coupon = await _couponRepo.GetByIdAsync(id);
        return await _couponRepo.DeleteAsync(id);
    }
   
} 