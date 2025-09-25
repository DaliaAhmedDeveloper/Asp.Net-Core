namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class ProductTranslationConfiguration : IEntityTypeConfiguration<ProductTranslation>
{
    public void Configure(EntityTypeBuilder<ProductTranslation> builder)
    {
        /*
        int Id  
        string Name  
        string Description  
        string Brand  
        string LanguageCode  
        int ProductId  
        */

        // Table name (optional)
        builder.ToTable("ProductTranslations");

        builder.HasKey(pt => pt.Id);
        builder.Property(pt => pt.Name).IsRequired().HasMaxLength(100);
        builder.Property(pt => pt.Description).HasMaxLength(1000);
        builder.Property(pt => pt.Brand).HasMaxLength(100);
        builder.Property(pt => pt.LanguageCode).IsRequired().HasMaxLength(10);
        builder.HasOne(pt => pt.Product)
               .WithMany(p => p.Translations)
               .HasForeignKey(pt => pt.ProductId)
               .OnDelete(DeleteBehavior.Cascade)
               ;
    }
}