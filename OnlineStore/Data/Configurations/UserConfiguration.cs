namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
     public void Configure(EntityTypeBuilder<User> builder)
     {
          /*
          int Id  
          string FullName  
          string Email  
          string PasswordHash  
          string Role  
          ProviderType Provider
          string PhoneNumber  
          bool IsActive  
          DateTime CreatedAt   
          */

          // Table name (optional)
          builder.ToTable("Users");

          builder.HasKey(u => u.Id);
          builder.Property(u => u.FullName).IsRequired().HasMaxLength(100);
          builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
          builder.Property(u => u.Provider).HasConversion<string>().HasMaxLength(100);
          builder.Property(u => u.PasswordHash).IsRequired();
          builder.Property(u => u.UserType).IsRequired().HasConversion<string>(); // to convert enum value from int to string 
          builder.Property(u => u.PhoneNumber).HasMaxLength(20);
          builder.Property(u => u.IsActive).IsRequired();
          builder.Property(u => u.CreatedAt).IsRequired();
          builder.HasMany(u => u.Roles).WithMany(r => r.Users);

          builder.HasIndex(u => u.Email);

          // builder.HasMany(c => c.Coupons)
          //      .WithMany(co => co.Users);

          builder.HasMany(u => u.Coupons)
               .WithMany(c => c.Users)
               .UsingEntity<CouponUser>(
               j => j
                    .HasOne(cu => cu.Coupon)
                    .WithMany(c => c.CouponUsers)
                    .HasForeignKey(cu => cu.CouponId)
                    .IsRequired(false), 
               j => j
                    .HasOne(cu => cu.User)
                    .WithMany(u => u.CouponUsers)
                    .HasForeignKey(cu => cu.UserId)
                    .IsRequired(false), 
               j =>
               {
                    j.ToTable("CouponUser");
                    j.HasKey(cu => cu.Id);
                    j.Property(cu => cu.UsedAt).HasDefaultValueSql("GETUTCDATE()");
                    j.Property(cu => cu.UsageCount).HasDefaultValue(1);
               }
          );

          builder.HasOne(u => u.Country)
               .WithMany(c => c.Users)
               .HasForeignKey(u => u.CountryId);

          builder.HasOne(u => u.City)
              .WithMany(c => c.Users)
              .HasForeignKey(u => u.CityId);

          builder.HasOne(u => u.State)
              .WithMany(c => c.Users)
               .HasForeignKey(u => u.StateId);

          builder.HasMany(u => u.StockMovements)
              .WithOne(sm => sm.User)
              .HasForeignKey(sm => sm.UserId)
              .IsRequired(false);

     }
}
