namespace OnlineStore.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;

public class AppSettingConfiguration : IEntityTypeConfiguration<AppSetting>
{
    public void Configure(EntityTypeBuilder<AppSetting> builder)
    {
         // Table name (optional)
        builder.ToTable("AppSettings");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Code)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(a => a.Code)
            .IsUnique();

        builder.Property(a => a.Value).HasMaxLength(2000);
    }
}
