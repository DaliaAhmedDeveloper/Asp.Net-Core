using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
       public void Configure(EntityTypeBuilder<OrderItem> builder)
       {


              /*
              int Id  
              int OrderId  
              int ProductId  
              int ProductVariantId  
              int Quantity 
              int points
              decimal WalletAmount 
              decimal UnitPrice 
              bool IsReviewed 
              bool IsReturned
              */

              // Table name (optional)
              builder.ToTable("OrderItems");

              builder.HasKey(oi => oi.Id);

              builder.Property(oi => oi.UnitPrice)
                      .HasColumnType("decimal(18,2)");

              builder.Property(oi => oi.WalletAmount)
              .HasColumnType("decimal(18,2)")
              .HasDefaultValue(0);

              // relations
              builder.HasOne(oi => oi.Order)
               .WithMany(o => o.OrderItems)
               .HasForeignKey(oi => oi.OrderId)
               .OnDelete(DeleteBehavior.Cascade).IsRequired(false);


              builder.HasOne(oi => oi.ProductVariant)
                     .WithMany(pv => pv.OrderItems)
                     .HasForeignKey(oi => oi.ProductVariantId)
                     .OnDelete(DeleteBehavior.NoAction);

              builder.HasOne(oi => oi.Product)
                     .WithMany(p => p.OrderItems)
                     .HasForeignKey(oi => oi.ProductId)
                     .OnDelete(DeleteBehavior.NoAction).IsRequired(false);
       }
}
