namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class WalletTransactionConfiguration : IEntityTypeConfiguration<WalletTransaction>
{
    public void Configure(EntityTypeBuilder<WalletTransaction> builder)
    {
        /*
        int Id  
        int WalletId  
        Wallet Wallet  
        decimal Amount  
        string Description  
        DateTime CreatedAt  
        */

        // Table name (optional)
        builder.ToTable("WalletTransactions");

        builder.HasKey(wt => wt.Id);
        builder.Property(wt => wt.Description).IsRequired().HasMaxLength(100);
        builder.Property(wt => wt.Amount).HasPrecision(18, 4).IsRequired();
        builder.Property(wt => wt.CreatedAt).IsRequired();
    }
}