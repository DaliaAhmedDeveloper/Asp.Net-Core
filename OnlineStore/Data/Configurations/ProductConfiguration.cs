namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
       public void Configure(EntityTypeBuilder<Product> builder)
       {
              /*
              int Id  
              string Slug  
              string SKU  
              ProductType Type  
              decimal Price 
              decimal? SalePrice  
              string ImageUrl  
              DateTime CreatedAt   
              */

              // Table name (optional)
              builder.ToTable("Products");
              builder.HasKey(p => p.Id);
              builder.Property(p => p.Slug).IsRequired().HasMaxLength(100);
              builder.Property(p => p.SKU).IsRequired().HasMaxLength(100);
              builder.Property(p => p.Type).IsRequired().HasConversion<string>(); // to convert enum value from int to string into database 
              builder.Property(p => p.Price).HasPrecision(18, 4).IsRequired();
              builder.Property(p => p.SalePrice).HasPrecision(18, 4);
              builder.Property(p => p.ImageUrl).HasMaxLength(200);
              builder.Property(p => p.CreatedAt).IsRequired();

              // product categories many to many
              builder.HasMany(p => p.Categories)
                     .WithMany(c => c.Products);

              // products - tag many to many
              builder.HasMany(p => p.Tags)
                     .WithMany(t => t.Products);
       }
}