namespace OnlineStore.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class UserPointConfiguration : IEntityTypeConfiguration<UserPoint>
{
    public void Configure(EntityTypeBuilder<UserPoint> builder)
    {
        /* 
        Fields : 
        int Id
        int UserId 
        int Points
        PointType Type
        User User
        */

        // Table name (optional)
        builder.ToTable("UserPoints");
      
        builder.HasKey(a => a.Id);
        builder.HasOne(a => a.User)
               .WithMany(u => u.Points)
               .HasForeignKey(a => a.UserId)
               .OnDelete(DeleteBehavior.Cascade)
               ;
    }
}