namespace OnlineStore.Data.Seeders;

using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;

public static class RoleSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Slug = "SuperAdmin" },
            new Role { Id = 2, Slug = "Admin" }
        );

        // translations

        modelBuilder.Entity<RoleTranslation>().HasData(
            // Arabic Translations
            new RoleTranslation { Id = 1, RoleId = 1 , LanguageCode = "ar", Name = "مدير" , Description="مدير"},
            new RoleTranslation { Id = 2, RoleId = 2, LanguageCode = "ar", Name = "رئيس المديرين" , Description="رئيس المديرين"},

            // English Translations
            new RoleTranslation { Id = 3, RoleId = 1, LanguageCode = "en", Name = "Admin" , Description="Admin" },
            new RoleTranslation { Id = 4, RoleId = 2, LanguageCode = "en", Name = "Super Admin" , Description="Super Admin" }
        );

    }
}
