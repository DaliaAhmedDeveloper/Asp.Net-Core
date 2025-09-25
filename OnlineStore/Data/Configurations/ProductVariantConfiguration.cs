namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
       public void Configure(EntityTypeBuilder<ProductVariant> builder)
       {
              /*
              int Id  
              decimal Price  
              decimal? SalePrice  
              int Stock  
              string ImageUrl  
              int ProductId  
              bool IsDefault 
              */

              // Table name (optional)
              builder.ToTable("ProductVariants");
              builder.HasKey(pv => pv.Id);
              builder.Property(pv => pv.Price).HasPrecision(18, 2).IsRequired();
              builder.Property(pv => pv.SalePrice).HasPrecision(18, 2);
              builder.Property(pv => pv.ImageUrl).HasMaxLength(200);

              builder.HasIndex(pv => new { pv.Price, pv.SalePrice });

              builder.HasOne(pv => pv.Product)
                     .WithMany(p => p.ProductVariants)
                     .HasForeignKey(pv => pv.ProductId)
                     .OnDelete(DeleteBehavior.Cascade);
       }
}