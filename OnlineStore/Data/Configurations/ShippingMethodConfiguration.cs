namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;


public class ShippingMethodConfiguration : IEntityTypeConfiguration<ShippingMethod>
{
    public void Configure(EntityTypeBuilder<ShippingMethod> builder)
    {
        /*
        int Id  
        string Name  
        decimal Cost  
        string DeliveryTime   
        */

        // Table name (optional)
        builder.ToTable("ShippingMethods");

        builder.HasKey(sm => sm.Id);
        builder.Property(sm => sm.Name).IsRequired().HasMaxLength(100);
        builder.Property(sm => sm.Cost).HasPrecision(18, 4).IsRequired();
        builder.Property(sm => sm.DeliveryTime).HasMaxLength(100);
    }
}