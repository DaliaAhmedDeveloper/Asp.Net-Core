namespace OnlineStore.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        // Table name (optional)
        builder.ToTable("Cities");

        // Primary key
        builder.HasKey(c => c.Id);

        // Properties
        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(100);

        // Optionally make Name unique within a State
        builder.HasIndex(c => new { c.StateId, c.Name }).IsUnique();

        // Relationships

        // City -> State (Many-to-One)
        builder.HasOne(c => c.State)
               .WithMany(s => s.Cities)
               .HasForeignKey(c => c.StateId)
               .OnDelete(DeleteBehavior.Cascade)
               ;
    }
}
