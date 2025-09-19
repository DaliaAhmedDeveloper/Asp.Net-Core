namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class NotificationTranslationConfiguration : IEntityTypeConfiguration<NotificationTranslation>
{
    public void Configure(EntityTypeBuilder<NotificationTranslation> builder)
    {
        /*
        int Id  
        string Message  
        string Type  // Info, Warning, Error, etc.  
        string LanguageCode  
        int NotificationId  
        */

        // Table name (optional)
        builder.ToTable("NotificationTranslations");

        builder.HasKey(nt => nt.Id);
        builder.Property(nt => nt.Message).IsRequired();
        builder.Property(nt => nt.Title).IsRequired();

        builder.Property(nt => nt.LanguageCode).IsRequired().HasMaxLength(10);

        builder.HasOne(nt => nt.Notification)
               .WithMany(n => n.Translations)
               .OnDelete(DeleteBehavior.Cascade)
               .HasForeignKey(nt => nt.NotificationId).IsRequired(false);
    }
}