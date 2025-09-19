namespace OnlineStore.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class ReturnTrackingConfiguration : IEntityTypeConfiguration<ReturnTracking>
{
    public void Configure(EntityTypeBuilder<ReturnTracking> builder)
    {
        /* 
        Fields : 
        int Id
        int returnId
        ReturnStatus Status 
        string TrackingNumber
        string TrackingUrl
        DateTime UpdatedAt
        Return Return
       */

        builder.HasKey(rt => rt.Id);
        builder.HasOne(rt => rt.Return)
               .WithOne(r => r.ReturnTracking)
               .HasForeignKey<ReturnTracking>(rt => rt.ReturnId).IsRequired(false);
    }
}