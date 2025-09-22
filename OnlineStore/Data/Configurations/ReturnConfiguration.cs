namespace OnlineStore.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class ReturnConfiguration : IEntityTypeConfiguration<Return>
{
    public void Configure(EntityTypeBuilder<Return> builder)
    {
        /* 
        Fields : 
        int Id
        int OrderId
        Order Order
        int UserId
        User User
        string ReferenceNumber
        decimal TotalAmount
        string Reason
        ReturnStatus Status
        DateTime ReturnDate
        RefundType RefundType
        */

        // Table name (optional)
        builder.ToTable("Returns");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Reason).IsRequired().HasMaxLength(100);
        builder.Property(r => r.ReferenceNumber).IsRequired().HasMaxLength(255);
        builder.HasIndex(r => r.ReferenceNumber).IsUnique();
        builder.Property(r => r.Status).IsRequired();
        builder.Property(r => r.ReturnDate).IsRequired();
        builder.Property(r => r.TotalAmount).IsRequired().HasPrecision(18, 2);

         builder.HasOne(r => r.Order)
               .WithMany(o => o.Returns)
               .HasForeignKey(o => o.OrderId).IsRequired(false);
    }
}