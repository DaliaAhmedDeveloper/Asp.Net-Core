namespace OnlineStore.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        /* 
        Fields : 
        int Id
        string FullName
        string City
        string Country
        string Street
        string ZipCode
        bool IsDefault
        int UserId 
        */
        // Table name (optional)
        builder.ToTable("Addresses");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.FullName).IsRequired().HasMaxLength(100);
        builder.Property(a => a.City).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Country).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Street).IsRequired().HasMaxLength(200);
        builder.Property(a => a.ZipCode).HasMaxLength(20);
        builder.Property(a => a.IsDefault).IsRequired();
        builder.HasOne(a => a.User)
               .WithMany(u => u.Addresses)
               .HasForeignKey(a => a.UserId);
    }
}