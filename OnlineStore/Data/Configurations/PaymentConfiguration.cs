namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        /*
        int Id  
        decimal Amount  
        DateTime PaymentDate  
        int OrderId  
        */

        // Table name (optional)
        builder.ToTable("Payments");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Amount).HasPrecision(18, 4).IsRequired();
        builder.Property(p => p.PaymentDate).IsRequired();
        builder.HasOne(p => p.Order)
               .WithOne(o =>o.Payment)
               .HasForeignKey<Payment>( p => p.OrderId);
    }
}