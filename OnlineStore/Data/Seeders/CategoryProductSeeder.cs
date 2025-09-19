namespace OnlineStore.Data.Seeders;

using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;
public static class CategoryProductSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
        .HasMany(c => c.Products)
        .WithMany(p => p.Categories).UsingEntity(j => j.HasData(
            new { CategoriesId = 1, ProductsId = 1 },
            new { CategoriesId = 1, ProductsId = 2 },
            new { CategoriesId = 1, ProductsId = 3 },
            new { CategoriesId = 1, ProductsId = 4 },
            new { CategoriesId = 1, ProductsId = 5 },
            new { CategoriesId = 2, ProductsId = 4 },
            new { CategoriesId = 2, ProductsId = 5 },
            new { CategoriesId = 2, ProductsId = 6 },
            new { CategoriesId = 2, ProductsId = 7 },
            new { CategoriesId = 2, ProductsId = 8 },
            new { CategoriesId = 3, ProductsId = 1 },
            new { CategoriesId = 3, ProductsId = 6 },
            new { CategoriesId = 3, ProductsId = 9 },
            new { CategoriesId = 3, ProductsId = 10 },
            new { CategoriesId = 3, ProductsId = 11 },
            new { CategoriesId = 4, ProductsId = 12 },
            new { CategoriesId = 4, ProductsId = 13 },
            new { CategoriesId = 4, ProductsId = 14 },
            new { CategoriesId = 4, ProductsId = 15 },
            new { CategoriesId = 4, ProductsId = 16 },
            new { CategoriesId = 1, ProductsId = 17 },
            new { CategoriesId = 2, ProductsId = 18 },
            new { CategoriesId = 3, ProductsId = 19 },
            new { CategoriesId = 4, ProductsId = 20 },
            new { CategoriesId = 1, ProductsId = 6 },   // Shared Product
            new { CategoriesId = 2, ProductsId = 1 },   // Shared Product
            new { CategoriesId = 3, ProductsId = 2 },   // Shared Product
            new { CategoriesId = 4, ProductsId = 3 }    // Shared Product
        ));
    }
}
