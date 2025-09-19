namespace OnlineStore.Data.Seeders;

using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;
public static class ProductTagSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tag>()
        .HasMany(t => t.Products)
        .WithMany(p => p.Tags)
        .UsingEntity(j => j.HasData(
            new { TagsId = 1, ProductsId = 1 },
            new { TagsId = 1, ProductsId = 2 },
            new { TagsId = 2, ProductsId = 3 },
            new { TagsId = 2, ProductsId = 4 },
            new { TagsId = 3, ProductsId = 5 }
        ));
    }
}
