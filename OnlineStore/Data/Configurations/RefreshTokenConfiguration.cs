namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        /*
        int Id  
        string token
        int UserId
        DateTime ExpiryDate 
        bool IsRevoked 
        */

        // Table name (optional)
        builder.ToTable("RefreshTokens");
        
        builder.HasKey(rt => rt.Id);
        builder.Property(rt => rt.Token).IsRequired();
        builder.Property(rt => rt.UserId).IsRequired();
        builder.Property(rt => rt.ExpiryDate).IsRequired();
        builder.Property(rt => rt.IsRevoked).IsRequired();

        builder.HasIndex(rt =>  rt.Token);
        builder.HasOne(rt => rt.User)
               .WithOne(u => u.RefreshToken)
               .HasForeignKey<RefreshToken>(rt => rt.UserId);
    }
}