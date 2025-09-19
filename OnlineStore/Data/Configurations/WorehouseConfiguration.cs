using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
       
       // Table name (optional)
        builder.ToTable("warehouses");

        // Primary key
              builder.HasKey(w => w.Id);

        // Properties
        builder.Property(w => w.Code)
               .IsRequired()
               .HasMaxLength(20); // short code for warehouse

        builder.Property(w => w.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(w => w.Address)
        .IsRequired()
               .HasMaxLength(200);

        builder.Property(w => w.City)
        .IsRequired()
               .HasMaxLength(50);

        builder.Property(w => w.State)
        .IsRequired()
               .HasMaxLength(50);

        builder.Property(w => w.Country)
        .IsRequired()
               .HasMaxLength(50);

        builder.Property(w => w.ZipCode)
        .IsRequired()
               .HasMaxLength(20);

        builder.Property(w => w.Phone)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(w => w.Email)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(w => w.IsActive)
               .IsRequired()
               .HasDefaultValue(true);

        builder.Property(w => w.IsDefault)
               .IsRequired()
               .HasDefaultValue(false);

        // Indexes
        builder.HasIndex(w => w.Code).IsUnique(); // Warehouse code should be unique
        builder.HasIndex(w => w.Name); // Optional index for search by name

        // Relationships
        builder.HasMany(w => w.Stocks)
               .WithOne(s => s.Warehouse)
               .HasForeignKey(s => s.WarehouseId);
    }
}
