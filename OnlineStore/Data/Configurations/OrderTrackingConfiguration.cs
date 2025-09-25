namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class OrderTrackingConfiguration : IEntityTypeConfiguration<OrderTracking>
{
    public void Configure(EntityTypeBuilder<OrderTracking> builder)
    {
        /* 
        Fields : 
        int Id
        int OrderId
        OrderStatus Status 
        string TrackingNumber
        string TrackingUrl
        string DriverName
        string DriverPhone
        DateTime UpdatedAt
       */

        builder.HasKey(ot => ot.Id);
        builder.HasOne(ot => ot.Order)
               .WithOne(o => o.OrderTracking)
               .HasForeignKey<OrderTracking>(ot => ot.OrderId)
               .OnDelete(DeleteBehavior.Cascade)
               ;
    }
}