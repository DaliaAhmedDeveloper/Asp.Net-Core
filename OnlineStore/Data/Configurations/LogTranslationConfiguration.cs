namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class LogTranslationConfiguration : IEntityTypeConfiguration<LogTranslation>
{
    public void Configure(EntityTypeBuilder<LogTranslation> builder)
    {
        /*
        int Id  
        string Action  
        string Description  
        string LanguageCode  
        int LogId  
        */
        
        // Table name (optional)
        builder.ToTable("LogTranslations");

        builder.HasKey(lt => lt.Id);
        builder.Property(lt => lt.ExceptionTitle).IsRequired().HasMaxLength(100);
        builder.Property(lt => lt.ExceptionMessage).IsRequired();
        builder.Property(lt => lt.LanguageCode).IsRequired().HasMaxLength(10);
        builder.HasOne(lt => lt.Log)
               .WithMany(l => l.Translations)
               .HasForeignKey(lt => lt.LogId);
    }
}