namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class TagTranslationConfiguration : IEntityTypeConfiguration<TagTranslation>
{
    public void Configure(EntityTypeBuilder<TagTranslation> builder)
    {
        /*
        int Id  
        int TagId  
        string Name
        string LanguageCode  
        */
        
        // Table name (optional)
        builder.ToTable("TagTranslations");

        builder.HasKey(tt => tt.Id);
        builder.Property(tt => tt.Name).IsRequired().HasMaxLength(100);
        builder.Property(tt => tt.LanguageCode).IsRequired().HasMaxLength(10);
        builder.HasOne(tt => tt.Tag)
               .WithMany(t => t.Translations)
               .HasForeignKey(tt => tt.TagId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}