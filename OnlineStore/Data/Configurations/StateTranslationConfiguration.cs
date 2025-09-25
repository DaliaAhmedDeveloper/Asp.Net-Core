namespace OnlineStore.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;

public class StateTranslationConfiguration : IEntityTypeConfiguration<StateTranslation>
{
       public void Configure(EntityTypeBuilder<StateTranslation> builder)
       {
              // Table name (optional)
              builder.ToTable("StateTranslations");

              // Primary key
              builder.HasKey(c => c.Id);

              // Properties
              builder.Property(ct => ct.Name)
                     .IsRequired()
                     .HasMaxLength(100);

              // Optionally make Name unique within a State
              builder.HasIndex(ct => ct.LanguageCode);

              // Relationships

              // State -> StateTranslations (One-to-Many)
              builder.HasOne(ct => ct.State)
                     .WithMany(c => c.Translations)
                     .HasForeignKey(ct => ct.StateId)
                     .OnDelete(DeleteBehavior.Cascade)
                     ;
       }
}
