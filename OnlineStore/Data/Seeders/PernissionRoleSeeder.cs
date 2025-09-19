namespace OnlineStore.Data.Seeders;

using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;
public static class PermissionRoleSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // Assuming your permissions Ids start from 1 and go up to N
        var allPermissions = Enumerable.Range(1, 61) // 61 is total permissions we added earlier
                                       .Select(pid => new { RolesId = 1, PermissionsId = pid });

        modelBuilder.Entity<Role>()
            .HasMany(r => r.Permissions)
            .WithMany(p => p.Roles)
            .UsingEntity(j => j.HasData(allPermissions));
    }
}
