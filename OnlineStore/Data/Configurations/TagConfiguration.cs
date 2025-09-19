namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        /*
        int Id  
        string? Code 
        */

        // Table name (optional)
        builder.ToTable("Tags");
    
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Code).HasMaxLength(100);
    }
}