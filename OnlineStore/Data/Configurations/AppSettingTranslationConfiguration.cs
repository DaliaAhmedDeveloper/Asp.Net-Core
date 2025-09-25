namespace OnlineStore.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class AppSettingTranslationConfiguration : IEntityTypeConfiguration<AppSettingTranslation>
{
    public void Configure(EntityTypeBuilder<AppSettingTranslation> builder)
    {
        // Table name (optional)
        builder.ToTable("AppSettingTranslations");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Key)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(t => t.LanguageCode)
            .IsRequired()
            .HasMaxLength(10);

        builder.HasOne(at => at.AppSetting)
               .WithMany(a => a.Translations)
               .HasForeignKey(at => at.AppSettingId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
