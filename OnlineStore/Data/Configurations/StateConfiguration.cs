namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;

public class StateConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        // Table name (optional)
        builder.ToTable("States");

        // Primary key
        builder.HasKey(s => s.Id);

        // Properties
        builder.Property(s => s.Code)
               .IsRequired()
               .HasMaxLength(50);

        // Make Code unique
        builder.HasIndex(s => s.Code).IsUnique();

        // Relationships

        // State -> Country (Many-to-One)
        builder.HasOne(s => s.Country)
               .WithMany(c => c.States)
               .HasForeignKey(s => s.CountryId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
