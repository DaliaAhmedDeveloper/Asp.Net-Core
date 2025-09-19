namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class LogConfiguration : IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        /*
        int Id  
        DateTime CreatedAt  
        string PerformedBy  
        */

        // Table name (optional)
        builder.ToTable("Logs");
        builder.HasKey(l => l.Id);

        builder.Property(l => l.StackTrace).IsRequired().HasMaxLength(255);
        builder.Property(l => l.StackTrace).HasMaxLength(255);
        builder.Property(l => l.CreatedAt).IsRequired();
    }
}