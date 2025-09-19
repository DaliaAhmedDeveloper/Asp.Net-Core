using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
using OnlineStore.Models.Enums;

public class SupportTicketConfiguration : IEntityTypeConfiguration<SupportTicket>
{
    public void Configure(EntityTypeBuilder<SupportTicket> builder)
    {
       
       // Table name (optional)
        builder.ToTable("SupportTickets");

        // Primary key
              builder.HasKey(t => t.Id);

        // Properties
        builder.Property(t => t.TicketNumber)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(t => t.Subject)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(t => t.Description)
               .IsRequired()
               .HasMaxLength(2000);

        builder.Property(t => t.Resolution)
               .HasMaxLength(2000);

        // Enums stored as integers
        builder.Property(t => t.Priority)
               .IsRequired()
               .HasConversion<string>();

        builder.Property(t => t.Status)
               .IsRequired()
               .HasConversion<string>();

        builder.Property(t => t.Category)
               .IsRequired()
               .HasConversion<string>();

        // Indexes
        builder.HasIndex(t => t.TicketNumber).IsUnique();

        // Relationships
        builder.HasOne(t => t.User)
               .WithMany(u => u.SupportTickets)
               .HasForeignKey(t => t.UserId)
               .IsRequired();

        builder.HasOne(t => t.Order)
               .WithMany(o => o.SupportTickets)
               .HasForeignKey(t => t.OrderId)
               .IsRequired(false);

        builder.HasMany(t => t.Messages)
               .WithOne(m => m.Ticket)
               .HasForeignKey(m => m.TicketId)
               .IsRequired();
    }
}
