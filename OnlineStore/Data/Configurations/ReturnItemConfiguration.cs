namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class ReturnItemConfiguration : IEntityTypeConfiguration<ReturnItem>
{
       public void Configure(EntityTypeBuilder<ReturnItem> builder)
       {
              /* 
              Fields : 
              int Id  
              int ReturnId  
              int OrderItem
              int Quantity  
              decimal UnitPrice  
              decimal Subtotal  
              String Reason
              */

              // Table name (optional)
              builder.ToTable("ReturnItems");

              builder.HasKey(r => r.Id);

              builder.Property(r => r.Quantity).IsRequired();
              builder.Property(r => r.Reason).IsRequired().HasMaxLength(100);

              builder.Property(r => r.UnitPrice).IsRequired().HasPrecision(18, 2); // For currency
              builder.Ignore(r => r.Subtotal);

              builder.HasOne(ri => ri.Return)
                     .WithMany(r => r.ReturnItems)
                     .HasForeignKey(r => r.ReturnId);

              builder.HasOne(ri => ri.OrderItem)
                     .WithOne(oi => oi.ReturnItem)
                     .HasForeignKey<ReturnItem>(ri => ri.OrderItemId)
                     .OnDelete(DeleteBehavior.Restrict)
                     .IsRequired();
       }
}