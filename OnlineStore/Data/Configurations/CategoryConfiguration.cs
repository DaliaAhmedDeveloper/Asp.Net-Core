namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
     public void Configure(EntityTypeBuilder<Category> builder)
     {
          /*
          int Id  
          string Slug  
          int? ParentId  
          bool IsDeal  
          */

          // Table name (optional)
          builder.ToTable("Categories");

          builder.HasKey(c => c.Id);
          builder.Property(c => c.Slug).IsRequired().HasMaxLength(100);
          builder.Property(c => c.IsDeal).IsRequired();
          builder.HasOne(c => c.Parent)
               .WithMany(c => c.Children)
               .HasForeignKey(c => c.ParentId).OnDelete(DeleteBehavior.Restrict);

          builder.HasMany(c => c.Coupons)
               .WithMany(co => co.Categoies);
     }
}