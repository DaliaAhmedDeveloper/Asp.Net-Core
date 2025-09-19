namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;

public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.ProductVariantId).IsRequired();
        builder.Property(s => s.WarehouseId).IsRequired();
        builder.Property(s => s.TotalQuantity).IsRequired();
        builder.Property(s => s.ReservedQuantity).IsRequired();
        builder.Property(s => s.MinimumStockLevel).IsRequired();
        builder.Property(s => s.UnitCost).HasPrecision(18, 4).IsRequired();
        builder.Property(s => s.LastRestocked).IsRequired();
        builder.Property(s => s.LastStockCount).IsRequired();
        
        // Indexes
        builder.HasIndex(s => new { s.ProductVariantId, s.WarehouseId });
        
        // Relationships
        builder.HasOne(s => s.ProductVariant)
               .WithOne(pv => pv.Stock)
               .HasForeignKey<Stock>(s => s.ProductVariantId);
    }
} 