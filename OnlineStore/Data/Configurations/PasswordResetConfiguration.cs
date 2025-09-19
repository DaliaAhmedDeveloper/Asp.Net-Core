namespace OnlineStore.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class PasswordResetConfiguration : IEntityTypeConfiguration<PasswordReset>
{
    public void Configure(EntityTypeBuilder<PasswordReset> builder)
    {
      
      
      // Table name (optional)
        builder.ToTable("PasswordResets");

       // Primary key
              builder.HasKey(pr => pr.Id);

        // Email column
        builder.Property(pr => pr.Email)
               .IsRequired()         // NOT NULL
               .HasMaxLength(255);   // Optional: limit length

        // Token column
        builder.Property(pr => pr.Token)
               .IsRequired()         // NOT NULL
               .HasMaxLength(500);

        // CreatedAt column
        builder.Property(pr => pr.CreatedAt)
               .IsRequired();

        // ExpiresAt column
        builder.Property(pr => pr.ExpiresAt)
               .IsRequired();

        // Used column
        builder.Property(pr => pr.Used)
               .IsRequired()
               .HasDefaultValue(false);
    }
}