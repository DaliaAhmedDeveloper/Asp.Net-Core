namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
       public void Configure(EntityTypeBuilder<CartItem> builder)
       {
              /*
              int Id 
              int Quantity
              int CartId
              int? VariantId 
              int ProductId
              */
              // Table name (optional)
              builder.ToTable("CartItems");

              builder.HasKey(ci => ci.Id);
              builder.Property(ci => ci.Quantity).IsRequired();
              builder.HasOne(ci => ci.Cart)
                     .WithMany(c => c.Items)
                     .HasForeignKey(ci => ci.CartId);
              builder.HasOne(ci => ci.ProductVariant)
                     .WithMany()
                     .HasForeignKey(ci => ci.VariantId);
              builder.HasOne(ci => ci.Product)
                     .WithMany()
                     .HasForeignKey(ci => ci.ProductId).IsRequired(false);
       }
}