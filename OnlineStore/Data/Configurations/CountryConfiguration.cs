namespace OnlineStore.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        // Table name (optional)
        builder.ToTable("Countries");

        // Primary key
        builder.HasKey(c => c.Id);

        // Properties
        builder.Property(c => c.Code)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(c => c.PhoneCode)
               .HasMaxLength(10);

        builder.Property(c => c.IsActive)
               .IsRequired();

        // Make Code unique
        builder.HasIndex(c => c.Code).IsUnique();

              // Relationships

              // Country -> States (One-to-Many)
              builder.HasMany(c => c.States)
                     .WithOne(s => s.Country)
                     .HasForeignKey(s => s.CountryId)
                     .OnDelete(DeleteBehavior.Cascade)
                     ;
    }
}
