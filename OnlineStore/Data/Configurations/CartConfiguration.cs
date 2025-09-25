namespace OnlineStore.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;


public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        /*
        int Id
        int UserId
        */
        // Table name (optional)
        builder.ToTable("Carts");

        builder.HasKey(c => c.Id);
        builder.HasOne(c => c.User)
               .WithOne(u => u.Cart)
               .HasForeignKey<Cart>(c => c.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}