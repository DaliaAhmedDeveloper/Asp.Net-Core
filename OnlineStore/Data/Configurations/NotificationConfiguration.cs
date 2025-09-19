namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        /*
        int Id  
        string Message  
        string Type  // Info, Warning, Error, etc.  
        bool IsRead  
        DateTime CreatedAt  
        int UserId  
        */

        // Table name (optional)
        builder.ToTable("Notifications");

        builder.HasKey(n => n.Id);
        builder.Property(n => n.Type).IsRequired();
        builder.Property(n => n.Type).IsRequired().HasMaxLength(50);
        builder.Property(n => n.CreatedAt).IsRequired();
        builder.HasOne(n => n.User)
               .WithMany(u => u.Notifications)
               .HasForeignKey(n => n.UserId);
    }
}