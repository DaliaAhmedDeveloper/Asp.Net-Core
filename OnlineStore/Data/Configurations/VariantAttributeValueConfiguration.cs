namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class VariantAttributeValueConfiguration : IEntityTypeConfiguration<VariantAttributeValue>
{
       public void Configure(EntityTypeBuilder<VariantAttributeValue> builder)
       {
              /*
              int Id  
              int ProductVariantId  
              int AttributeId  
              int AttributeValueId  
              */

              // Table name (optional)
              builder.ToTable("VariantAttributeValues");

              builder.HasKey(vav => vav.Id);
              builder.HasOne(vav => vav.ProductVariant)
                     .WithMany(pv => pv.VariantAttributeValues)
                     .HasForeignKey(vav => vav.ProductVariantId).OnDelete(DeleteBehavior.Restrict);
              builder.HasOne(vav => vav.Attribute)
                     .WithMany(pa => pa.VariantAttributeValues)
                     .HasForeignKey(vav => vav.AttributeId).OnDelete(DeleteBehavior.Restrict);
              builder.HasOne(vav => vav.AttributeValue)
                     .WithMany(av => av.VariantAttributeValues)
                     .HasForeignKey(vav => vav.AttributeValueId).OnDelete(DeleteBehavior.Restrict);
       }
}