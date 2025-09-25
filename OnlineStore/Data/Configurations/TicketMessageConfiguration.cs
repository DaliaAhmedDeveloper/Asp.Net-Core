using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;

public class TicketMessageConfiguration : IEntityTypeConfiguration<TicketMessage>
{
    public void Configure(EntityTypeBuilder<TicketMessage> builder)
    {
       // Table name (optional)
        builder.ToTable("TicketMessages");

        // Primary key
              builder.HasKey(tm => tm.Id);

        // Properties
        builder.Property(tm => tm.Message)
               .IsRequired()
               .HasMaxLength(2000); 

        builder.Property(tm => tm.IsFromStaff)
               .IsRequired()
               .HasDefaultValue(false);

        builder.Property(tm => tm.IsInternal)
               .IsRequired()
               .HasDefaultValue(false);

        builder.Property(tm => tm.IsRead)
               .IsRequired()
               .HasDefaultValue(false);

        builder.Property(tm => tm.ReadAt);

              // Relationships
              builder.HasOne(tm => tm.Ticket)
                     .WithMany(t => t.Messages)
                     .HasForeignKey(tm => tm.TicketId)
                     .OnDelete(DeleteBehavior.Cascade);

              builder.HasOne(tm => tm.User)
              .WithMany(u => u.TicketMessages)
              .HasForeignKey(tm => tm.UserId)
              .OnDelete(DeleteBehavior.NoAction);
    }
}
