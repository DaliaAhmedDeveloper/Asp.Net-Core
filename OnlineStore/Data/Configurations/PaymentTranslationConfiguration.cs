namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class PaymentTranslationConfiguration : IEntityTypeConfiguration<PaymentTranslation>
{
    public void Configure(EntityTypeBuilder<PaymentTranslation> builder)
    {

        /*
        int Id  
        string Method  
        string Status  
        string LanguageCode  
        int PaymentId  
        */

        // Table name (optional)
        builder.ToTable("PaymentTransltions");


        builder.HasKey(pt => pt.Id);
        builder.Property(pt => pt.Method).IsRequired().HasMaxLength(50);
        builder.Property(pt => pt.Status).IsRequired().HasMaxLength(50);
        builder.Property(pt => pt.LanguageCode).IsRequired().HasMaxLength(10);
        builder.HasOne(pt => pt.Payment)
               .WithMany(p => p.Translations)
               .HasForeignKey(pt => pt.PaymentId)
               .OnDelete(DeleteBehavior.Cascade)
               ;
    }
}