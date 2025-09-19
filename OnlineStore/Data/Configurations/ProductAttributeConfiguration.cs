namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
{
       public void Configure(EntityTypeBuilder<ProductAttribute> builder)
       {
              /*
              int Id  
              string? Code  
              */

              // Table name (optional)
              builder.ToTable("ProductAttributes");

              builder.HasKey(pa => pa.Id);
              builder.Property(pa => pa.Code).HasMaxLength(100);
       }
}