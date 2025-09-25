namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class AttributeValueConfiguration : IEntityTypeConfiguration<AttributeValue>
{
    public void Configure(EntityTypeBuilder<AttributeValue> builder)
    {
        /*
        int Id
        int AttributeId
        string? Code
        */

        // Table name (optional)
        builder.ToTable("AttributeValues");

        builder.HasKey(av => av.Id);
        builder.Property(av => av.Code).HasMaxLength(100);
        builder.HasOne(av => av.Attribute)
               .WithMany(pa => pa.Values)
               .HasForeignKey(av => av.AttributeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}