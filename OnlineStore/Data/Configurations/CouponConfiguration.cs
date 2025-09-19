namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
       public void Configure(EntityTypeBuilder<Coupon> builder)
       {
              /*
              int Id  
              string Code  
              DiscountType DiscountType  
              decimal DiscountValue  
              DateTime StartDate  
              DateTime EndDate  
              int UsageLimit  
              int UsedCount  
              */

              // Table name (optional)
              builder.ToTable("Coupons");
              
              // Primary Key
              builder.HasKey(c => c.Id);

              // Properties
              builder.Property(c => c.Code)
                     .IsRequired()
                     .HasMaxLength(50);

              // Add unique constraint
              builder.HasIndex(c => c.Code)
                     .IsUnique();


              builder.Property(c => c.DiscountType)
                     .IsRequired();

              builder.Property(c => c.DiscountValue)
                     .HasColumnType("decimal(18,2)");

              builder.Property(c => c.DiscountPrecentage)
                     .HasColumnType("int");

              builder.Property(c => c.MaxUsagePerUser)
                     .HasDefaultValue(0);

              builder.Property(c => c.MaxDiscountAmount)
                     .HasColumnType("decimal(18,2)");

              builder.Property(c => c.MinimumOrderAmount)
                     .HasColumnType("decimal(18,2)");

              builder.Property(c => c.IsActive)
                     .IsRequired();

              builder.Property(c => c.IsForFirstOrderOnly)
                     .IsRequired();

              builder.Property(c => c.StartDate)
                     .IsRequired();

              builder.Property(c => c.EndDate)
                     .IsRequired();

              // one coupon has many translations
              builder.HasMany(c => c.Translations)
               .WithOne(t => t.Coupon)
               .HasForeignKey(t => t.CouponId);

              // many to many
              builder.HasMany(c => c.Categoies)
                     .WithMany(cat => cat.Coupons);

       }
}