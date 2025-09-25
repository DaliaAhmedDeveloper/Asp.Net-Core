namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class PermissionConTranslationfiguration : IEntityTypeConfiguration<PermissionTranslation>
{
       public void Configure(EntityTypeBuilder<PermissionTranslation> builder)
       {
              /*
              int Id  
              int RoleId
              string Name
              string Description
              string LanguageCode
              */


              // Table name (optional)
              builder.ToTable("PermissionTranslations");

              builder.HasKey(pt => pt.Id); 
              builder.Property(pt => pt.Name).IsRequired().HasMaxLength(100);
              builder.Property(pt => pt.Description).IsRequired().HasMaxLength(100);
              builder.Property(pt => pt.LanguageCode).IsRequired().HasMaxLength(50);
              builder.HasOne(t => t.Permission)
                    .WithMany(p => p.Translations)
                    .HasForeignKey(t => t.PermissionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    ;

       }
}