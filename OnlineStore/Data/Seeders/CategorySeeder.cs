namespace OnlineStore.Data.Seeders;

using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;
public static class CategorySeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
             new Category { Id = 1, Slug = "Uncategorized"  , ImageUrl ="default.png"},
             new Category { Id = 2, Slug = "electronics", ImageUrl = "default.png" },
             new Category { Id = 3, Slug = "tV", ParentId = 1  , ImageUrl ="default.png"},
             new Category { Id = 4, Slug = "laptops", ParentId = 1 , ImageUrl ="default.png" },
             new Category { Id = 5, Slug = "fridges", ParentId = 1  , ImageUrl ="default.png"}
         );

        modelBuilder.Entity<CategoryTranslation>().HasData(
            new CategoryTranslation { Id = 6, CategoryId = 1, LanguageCode = "en", Name = "UnCategorized", Description = "Dummy text for the printing and typesetting industry." },
            new CategoryTranslation { Id = 7, CategoryId = 2, LanguageCode = "en", Name = "Electronics", Description = "Dummy text for the printing and typesetting industry." },
            new CategoryTranslation { Id = 8, CategoryId = 3, LanguageCode = "en", Name = "TV", Description = "Dummy text for the printing and typesetting industry." },
            new CategoryTranslation { Id = 9, CategoryId = 4, LanguageCode = "en", Name = "Laptops", Description = "Dummy text for the printing and typesetting industry." },
            new CategoryTranslation { Id = 10, CategoryId = 5, LanguageCode = "en", Name = "Fridges", Description = "Dummy text for the printing and typesetting industry." },

            new CategoryTranslation { Id = 1, CategoryId = 1, LanguageCode = "ar", Name = "غير مصنف", Description = "نص تجريبي لصناعة الطباعة والتنضيد." },
            new CategoryTranslation { Id = 2, CategoryId = 2, LanguageCode = "ar", Name = "إلكترونيات", Description = "نص تجريبي لصناعة الطباعة والتنضيد." },
            new CategoryTranslation { Id = 3, CategoryId = 3, LanguageCode = "ar", Name = "تلفزيون", Description = "نص تجريبي لصناعة الطباعة والتنضيد." },
            new CategoryTranslation { Id = 4, CategoryId = 4, LanguageCode = "ar", Name = "حاسبات محمولة", Description = "نص تجريبي لصناعة الطباعة والتنضيد." },
            new CategoryTranslation { Id = 5, CategoryId = 5, LanguageCode = "ar", Name = "ثلاجات", Description = "نص تجريبي لصناعة الطباعة والتنضيد." }
        );

    }
}
