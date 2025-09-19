namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class CouponTranslationConfiguration : IEntityTypeConfiguration<CouponTranslation>
{
    public void Configure(EntityTypeBuilder<CouponTranslation> builder)
    {

        /*
        int Id  
        string Description  
        string TermsAndConditions
        int CategoryId  
        string LanguageCode  
        */

        // Table name (optional)
        builder.ToTable("CouponTranslations");

        builder.HasKey(ct => ct.Id);
        builder.Property(ct => ct.Description).IsRequired();
        builder.Property(ct => ct.TermsAndConditions).HasMaxLength(255);
        builder.Property(ct => ct.LanguageCode).IsRequired().HasMaxLength(10);
        builder.HasOne(ct => ct.Coupon)
               .WithMany(ct => ct.Translations)
               .HasForeignKey(ct => ct.CouponId).IsRequired(false);
    }
}
