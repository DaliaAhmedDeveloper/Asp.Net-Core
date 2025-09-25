namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class CategoryTranslationConfiguration : IEntityTypeConfiguration<CategoryTranslation>
{
    public void Configure(EntityTypeBuilder<CategoryTranslation> builder)
    {

        /*
        int Id  
        string Name  
        string Description  
        int CategoryId  
        string LanguageCode  
        */

        // Table name (optional)
        builder.ToTable("CategoryTranslations");

        builder.HasKey(ct => ct.Id);
        builder.Property(ct => ct.Name).IsRequired().HasMaxLength(100);
        builder.Property(ct => ct.Description).IsRequired();
        builder.Property(ct => ct.LanguageCode).IsRequired().HasMaxLength(10);

        builder.HasOne(ct => ct.Category)
               .WithMany(c => c.Translations)
               .HasForeignKey(ct => ct.CategoryId)
               .OnDelete(DeleteBehavior.Cascade)
               ;
    }
}
