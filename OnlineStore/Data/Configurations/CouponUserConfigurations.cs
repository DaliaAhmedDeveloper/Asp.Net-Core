// namespace OnlineStore.Data.Configurations;

// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using OnlineStore.Models;
// public class CouponUserConfiguration : IEntityTypeConfiguration<CouponUser>
// {
//     public void Configure(EntityTypeBuilder<CouponUser> builder)
//     {

//         builder.ToTable("CouponUser");

//         builder.HasOne(cu => cu.User)
//             .WithMany(u => u.CouponUsers)
//             .HasForeignKey(uc => uc.UserId).IsRequired(false);

//         builder.HasOne(cu => cu.Coupon)
//             .WithMany(c => c.CouponUsers)
//             .HasForeignKey(uc => uc.CouponId).IsRequired(false);
//     }
// }