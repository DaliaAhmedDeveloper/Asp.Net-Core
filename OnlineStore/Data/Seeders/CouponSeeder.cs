namespace OnlineStore.Data.Seeders;

using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Models.Enums;

public static class CouponSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            // 1. First Order Only Coupon
            new Coupon
            {
                Id = 1,
                Code = "FIRST50",
                DiscountType = DiscountType.Percentage,
                DiscountPrecentage = 50,
                DiscountValue = null,
                MaxUsagePerUser = 1,
                MaxDiscountAmount = 100,
                MinimumOrderAmount = 50,
                IsActive = true,
                IsForFirstOrderOnly = true
            },

            // 2. Percentage Coupon
            new Coupon
            {
                Id = 2,
                Code = "SAVE20",
                DiscountType = DiscountType.Percentage,
                DiscountPrecentage = 20,
                DiscountValue = null,
                MaxUsagePerUser = 5,
                MaxDiscountAmount = 200,
                MinimumOrderAmount = 100,
                IsActive = true,
                IsForFirstOrderOnly = false
            },

            // 3. Fixed Value Coupon
            new Coupon
            {
                Id = 3,
                Code = "FLAT100",
                DiscountType = DiscountType.Fixed,
                DiscountValue = 100,
                DiscountPrecentage = null,
                MaxUsagePerUser = 3,
                MaxDiscountAmount = 100,
                MinimumOrderAmount = 200,
                IsActive = true,
                IsForFirstOrderOnly = false
            }
        );
    }
}
