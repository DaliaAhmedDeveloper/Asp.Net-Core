namespace OnlineStore.Data.Seeders;

using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;
public static class UserRoleSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
        .HasMany(u => u.Roles)
        .WithMany(r => r.Users).UsingEntity(j => j.HasData(
            new { UsersId = 1, RolesId = 1 }
        ));
    }
}
