namespace OnlineStore.Data.Seeders;

using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;
public static class ProductVariantSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductVariant>().HasData(
            new ProductVariant { Id = 1, ImageUrl = "default.png", ProductId = 1 },
            new ProductVariant { Id = 2,  ImageUrl = "default.png", ProductId = 1 },
            new ProductVariant { Id = 3, ImageUrl = "default.png", ProductId = 2 },
            new ProductVariant { Id = 4, ImageUrl = "default.png",  ProductId = 2 },
            new ProductVariant { Id = 5, ImageUrl = "default.png",  ProductId = 3 },
            new ProductVariant { Id = 6, ImageUrl = "default.png", ProductId = 3 },
            new ProductVariant { Id = 7, ImageUrl = "default.png",  ProductId = 4 },
            new ProductVariant { Id = 8, ImageUrl = "default.png",  ProductId = 4 },
            new ProductVariant { Id = 9, ImageUrl = "default.png",  ProductId = 5 },
            new ProductVariant { Id = 10, ImageUrl = "default.png", ProductId = 5 }
        );
    }
}
