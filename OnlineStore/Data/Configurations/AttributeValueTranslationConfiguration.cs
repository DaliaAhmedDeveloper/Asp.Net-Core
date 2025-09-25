namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;


public class AttributeValueTranslationConfiguration : IEntityTypeConfiguration<AttributeValueTranslation>
{
    public void Configure(EntityTypeBuilder<AttributeValueTranslation> builder)
    {
        /*
        int Id 
        string Name
        string LanguageCode
        int AttributeValueId 
        */

        // Table name (optional)
        builder.ToTable("AttributeValueTranslations");

        builder.HasKey(avt => avt.Id);
        builder.Property(avt => avt.Name).IsRequired().HasMaxLength(100);
        builder.Property(avt => avt.LanguageCode).IsRequired().HasMaxLength(10);
        builder.HasOne(avt => avt.AttributeValue)
               .WithMany(av => av.Translations)
               .HasForeignKey(avt => avt.AttributeValueId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}