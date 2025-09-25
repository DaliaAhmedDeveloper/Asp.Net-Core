namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class RoleTranslationConfiguration : IEntityTypeConfiguration<RoleTranslation>
{
       public void Configure(EntityTypeBuilder<RoleTranslation> builder)
       {
              /*
              int Id  
              int RoleId
              string Name
              string Description
              string LanguageCode
              */

              // Table name (optional)
              builder.ToTable("RoleTranslations");

              builder.HasKey(rt => rt.Id);
              builder.Property(rt => rt.Name).IsRequired().HasMaxLength(100);
              builder.Property(rt => rt.Description).IsRequired().HasMaxLength(100);
              builder.Property(rt => rt.LanguageCode).IsRequired().HasMaxLength(50);


              builder.HasOne(t => t.Role)
                    .WithMany(r => r.Translations)
                    .HasForeignKey(t => t.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);

       }
}