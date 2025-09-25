namespace OnlineStore.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;

public class CountryTranslationConfiguration : IEntityTypeConfiguration<CountryTranslation>
{
    public void Configure(EntityTypeBuilder<CountryTranslation> builder)
    {
        // Table name (optional)
              builder.ToTable("CountryTranslations");

              // Primary key
              builder.HasKey(c => c.Id);

              // Properties
              builder.Property(ct => ct.Name)
                     .IsRequired()
                     .HasMaxLength(100);

              // Optionally make Name unique within a State
              builder.HasIndex(ct => ct.LanguageCode);

        // Relationships

        // City -> CityTranslations (One-to-Many)
        builder.HasOne(ct => ct.Country)
               .WithMany(c => c.Translations)
               .HasForeignKey(ct => ct.CountryId)
               .OnDelete(DeleteBehavior.Cascade)
                ;
    }
}
