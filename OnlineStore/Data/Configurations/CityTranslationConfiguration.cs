namespace OnlineStore.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;

public class CityTranslationConfiguration : IEntityTypeConfiguration<CityTranslation>
{
       public void Configure(EntityTypeBuilder<CityTranslation> builder)
       {
              // Table name (optional)
              builder.ToTable("CityTranslations");

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
              builder.HasOne(ct => ct.City)
                     .WithMany(c => c.Translations)
                     .HasForeignKey(ct => ct.CityId)
                     .OnDelete(DeleteBehavior.Cascade)
                     ;
       }
}
