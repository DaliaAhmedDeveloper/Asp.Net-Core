namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
       public void Configure(EntityTypeBuilder<Role> builder)
       {
              /*
              int Id  
              string Slug
              */

              // Table name (optional)
              builder.ToTable("Roles");

              builder.HasKey(r => r.Id); // question :  why only assign it here to primary ??
              builder.Property(r => r.Slug).IsRequired().HasMaxLength(100);

              // role permission many to many
              builder.HasMany(r => r.Permissions)
                     .WithMany(p => p.Roles);
       }
}