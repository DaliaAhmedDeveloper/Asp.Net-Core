namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
       public void Configure(EntityTypeBuilder<Permission> builder)
       {
              /*
              int Id  
              string Slug
              */
              
              // Table name (optional)
              builder.ToTable("Permissions");

              builder.HasKey(p => p.Id);
              builder.Property(p => p.Slug).IsRequired().HasMaxLength(100);
       }
}