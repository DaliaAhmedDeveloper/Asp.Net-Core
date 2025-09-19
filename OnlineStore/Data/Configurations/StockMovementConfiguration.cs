namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;

public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
{
    public void Configure(EntityTypeBuilder<StockMovement> builder)
    {

        // Table name (optional)
        builder.ToTable("StockMovements");

        builder.HasKey(sm => sm.Id);
        builder.Property(sm => sm.StockId).IsRequired();
        builder.Property(sm => sm.Quantity).IsRequired();
        builder.Property(sm => sm.Type).IsRequired();
        builder.Property(sm => sm.Reference).IsRequired().HasMaxLength(100);
        builder.Property(sm => sm.Notes).HasMaxLength(500);
        builder.Property(sm => sm.UnitCost).HasPrecision(18, 4).IsRequired();
        // Indexes
        builder.HasIndex(sm => sm.Type);
        builder.HasIndex(sm => sm.Reference);
        builder.HasIndex(sm => sm.CreatedAt);
        
        // Relationships
        builder.HasOne(sm => sm.Stock)
               .WithMany(s => s.StockMovements)
               .HasForeignKey(sm => sm.StockId)
               .IsRequired(false);
    }
} 