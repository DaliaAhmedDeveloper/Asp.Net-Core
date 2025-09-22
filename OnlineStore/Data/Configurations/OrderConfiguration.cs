namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
       public void Configure(EntityTypeBuilder<Order> builder)
       {
              /*
              int Id  
              decimal TotalAmount  
              OrderStatus  OrderStatus  
              string PaymentMethod  
              string ReferenceNumber
              DateTime CreatedAt  
              int pointsUsed
              decimal FinalAmount
              decimal WalletAmountUsed
              int UserId  
              string Items  
              string Coupon 
              string ShippingAddress
              ShippingMethod  ShippingMethod
              decimal CouponDiscountAmount
               decimal PointsDiscountAmount
              */

              // Table name (optional)
              builder.ToTable("Orders");

              builder.HasKey(o => o.Id);
              builder.Property(o => o.TotalAmountAfterSale).HasPrecision(18, 4).IsRequired();
              builder.Property(o => o.TotalAmountBeforeSale).HasPrecision(18, 4).IsRequired();
              builder.Property(o => o.SaleDiscountAmount).HasPrecision(18, 4).IsRequired();
              builder.Property(o => o.WalletAmountUsed).HasPrecision(18, 4).IsRequired();
              builder.Property(o => o.WalletAmountUsed).HasPrecision(18, 4).IsRequired();
              builder.Property(o => o.CouponDiscountAmount).HasPrecision(18, 4).IsRequired();
              builder.Property(o => o.PointsDiscountAmount).HasPrecision(18, 4).IsRequired();
              builder.Property(o => o.FinalAmount).HasPrecision(18, 4).IsRequired();
              builder.Property(o => o.PointsUsed).IsRequired();
              builder.Property(o => o.OrderStatus).HasConversion<string>().IsRequired().HasMaxLength(50);
              builder.Property(o => o.PaymentMethod).HasConversion<string>().IsRequired().HasMaxLength(50);
              builder.Property(o => o.ReferenceNumber).HasMaxLength(255);
              builder.HasIndex(o => o.ReferenceNumber).IsUnique();
              builder.Property(o => o.CreatedAt).IsRequired();
       }
}