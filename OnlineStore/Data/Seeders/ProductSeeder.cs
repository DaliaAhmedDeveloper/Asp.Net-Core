namespace OnlineStore.Data.Seeders;

using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;
using System;

public static class ProductSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Slug = "smart-tv", Price = 1500, SKU = "SKU1001", ImageUrl = "default.png", CreatedAt = new DateTime(2024, 1, 1) },
            new Product { Id = 2, Slug = "wireless-headphones", Price = 200, SKU = "SKU1002", ImageUrl = "default.png", CreatedAt = new DateTime(2024, 1, 1) },
            new Product { Id = 3, Slug = "laptop-pro-15", Price = 2500, SKU = "SKU1003", ImageUrl = "default.png", CreatedAt = new DateTime(2024, 1, 1) },
            new Product { Id = 4, Slug = "smartphone-x12", Price = 999, SKU = "SKU1004", ImageUrl = "default.png", CreatedAt = new DateTime(2024, 1, 1) },
            new Product { Id = 5, Slug = "gaming-console-z", Price = 500, SKU = "SKU1005", ImageUrl = "default.png", CreatedAt = new DateTime(2024, 1, 1) },
            new Product { Id = 6, Slug = "bluetooth-speaker", Price = 80, SKU = "SKU1006", ImageUrl = "default.png", CreatedAt = new DateTime(2024, 1, 1) },
            new Product { Id = 7, Slug = "4k-action-camera", Price = 300, SKU = "SKU1007", ImageUrl = "default.png", CreatedAt = new DateTime(2024, 1, 1) },
            new Product { Id = 8, Slug = "smart-watch-s9", Price = 299, SKU = "SKU1008", ImageUrl = "default.png", CreatedAt = new DateTime(2024, 1, 1) },
            new Product { Id = 9, Slug = "vr-headset", Price = 350, SKU = "SKU1009", ImageUrl = "default.png", CreatedAt = new DateTime(2024, 1, 1) },
            new Product { Id = 10, Slug = "drone-camera", Price = 1200, SKU = "SKU1010", ImageUrl = "default.png", CreatedAt = new DateTime(2024, 1, 1) },
            new Product { Id = 11, Slug = "e-reader", Price = 150, SKU = "SKU1011", ImageUrl = "default.png", CreatedAt = new DateTime(2024, 1, 1) },
            new Product { Id = 12, Slug = "smart-home-hub", Price = 130, SKU = "SKU1012", ImageUrl = "default.png", CreatedAt = new DateTime(2024, 1, 1) },
            new Product { Id = 13, Slug = "wireless-router", Price = 120, SKU = "SKU1013", ImageUrl = "default.png", CreatedAt = new DateTime(2024, 1, 1) },
            new Product { Id = 14, Slug = "desktop-pc", Price = 1800, SKU = "SKU1014", ImageUrl = "default.png", CreatedAt = new DateTime(2024, 1, 1) },
            new Product { Id = 15, Slug = "portable-hard-drive", Price = 75, SKU = "SKU1015", ImageUrl = "default.png", CreatedAt = new DateTime(2024, 1, 1) },
            new Product { Id = 16, Slug = "noise-cancelling-earbuds", Price = 170, SKU = "SKU1016", ImageUrl = "default.png", CreatedAt = new DateTime(2024, 1, 1) },
            new Product { Id = 17, Slug = "smart-thermostat", Price = 220, SKU = "SKU1017", ImageUrl = "default.png", CreatedAt = new DateTime(2024, 1, 1) },
            new Product { Id = 18, Slug = "digital-camera", Price = 900, SKU = "SKU1018", ImageUrl = "default.png", CreatedAt = new DateTime(2024, 1, 1) },
            new Product { Id = 19, Slug = "tablet-pro", Price = 850, SKU = "SKU1019", ImageUrl = "default.png", CreatedAt = new DateTime(2024, 1, 1) },
            new Product { Id = 20, Slug = "smart-light-bulbs", Price = 60, SKU = "SKU1020", ImageUrl = "default.png", CreatedAt = new DateTime(2024, 1, 1) }
        );
    }
}
