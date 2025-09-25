namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        /*
        int Id  
        int UserId  
        decimal Balance  
        */

        // Table name (optional)
        builder.ToTable("Wallets");

        builder.HasKey(w => w.Id);
        builder.Property(w => w.UserId).IsRequired().HasMaxLength(100);
        builder.Property(w => w.Balance).HasPrecision(18, 4).IsRequired();
        builder.HasOne(w => w.User)
               .WithOne(u => u.Wallet)
               .HasForeignKey<Wallet>(w => w.UserId)
               .OnDelete(DeleteBehavior.Cascade)
               ;
    }
}