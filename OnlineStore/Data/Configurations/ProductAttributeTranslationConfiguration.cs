namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class ProductAttributeTranslationConfiguration : IEntityTypeConfiguration<ProductAttributeTranslation>
{
    public void Configure(EntityTypeBuilder<ProductAttributeTranslation> builder)
    {
        /*
        int Id  
        string Name  
        string LanguageCode  
        int AttributeId  
        */

        // Table name (optional)
        builder.ToTable("ProductAttributeTranslations");

        builder.HasKey(pat => pat.Id);
        builder.Property(pat => pat.Name).IsRequired().HasMaxLength(100);
        builder.Property(pat => pat.LanguageCode).IsRequired().HasMaxLength(10);
        builder.HasOne(pat => pat.Attribute)
               .WithMany(pa => pa.Translations)
               .HasForeignKey(pat => pat.ProductAttributeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}