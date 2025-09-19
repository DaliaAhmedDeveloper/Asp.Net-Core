namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;


public class ShippingMethodTranslationConfiguration : IEntityTypeConfiguration<ShippingMethodTranslation>
{
    public void Configure(EntityTypeBuilder<ShippingMethodTranslation> builder)
    {
        /*
        int Id  
        string Name  
        int ShippingMethodId  
        string LanguageCode  
        */

        // Table name (optional)
        builder.ToTable("ShippingMethodTranslations");

        builder.HasKey(smt => smt.Id);
        builder.Property(smt => smt.Name).IsRequired().HasMaxLength(100);
        builder.Property(smt => smt.LanguageCode).IsRequired().HasMaxLength(10);
        builder.HasOne(smt => smt.ShippingMethod)
               .WithMany(sm => sm.Translations)
               .HasForeignKey(smt => smt.ShippingMethodId).IsRequired(false);
    }
}
