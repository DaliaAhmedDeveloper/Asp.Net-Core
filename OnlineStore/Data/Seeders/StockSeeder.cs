namespace OnlineStore.Data.Seeders;

using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;
public static class StockSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Stock>().HasData(
            new Stock
            {
                Id = 1,
                ProductVariantId = 1,
                WarehouseId = 1,
                TotalQuantity = 100,
                ReservedQuantity = 10,
                MinimumStockLevel = 20,
                UnitCost = 50.0m,
            },
            new Stock
            {
                Id = 2,
                ProductVariantId = 2,
                WarehouseId = 1,
                TotalQuantity = 120,
                ReservedQuantity = 15,
                MinimumStockLevel = 25,
                UnitCost = 55.0m
            },
            new Stock
            {
                Id = 3,
                ProductVariantId = 3,
                WarehouseId = 1,
                TotalQuantity = 80,
                ReservedQuantity = 5,
                MinimumStockLevel = 10,
                UnitCost = 40.0m
            },
            new Stock
            {
                Id = 4,
                ProductVariantId = 4,
                WarehouseId = 1,
                TotalQuantity = 60,
                ReservedQuantity = 0,
                MinimumStockLevel = 10,
                UnitCost = 45.0m
            },
            new Stock
            {
                Id = 5,
                ProductVariantId = 5,
                WarehouseId = 1,
                TotalQuantity = 150,
                ReservedQuantity = 20,
                MinimumStockLevel = 30,
                UnitCost = 30.0m
            },
            new Stock
            {
                Id = 6,
                ProductVariantId = 6,
                WarehouseId = 1,
                TotalQuantity = 90,
                ReservedQuantity = 5,
                MinimumStockLevel = 15,
                UnitCost = 35.0m
            },
            new Stock
            {
                Id = 7,
                ProductVariantId = 7,
                WarehouseId = 1,
                TotalQuantity = 110,
                ReservedQuantity = 10,
                MinimumStockLevel = 20,
                UnitCost = 60.0m
            },
            new Stock
            {
                Id = 8,
                ProductVariantId = 8,
                WarehouseId = 1,
                TotalQuantity = 130,
                ReservedQuantity = 15,
                MinimumStockLevel = 25,
                UnitCost = 65.0m
            },
            new Stock
            {
                Id = 9,
                ProductVariantId = 9,
                WarehouseId = 1,
                TotalQuantity = 70,
                ReservedQuantity = 5,
                MinimumStockLevel = 10,
                UnitCost = 40.0m
            },
            new Stock
            {
                Id = 10,
                ProductVariantId = 10,
                WarehouseId = 1,
                TotalQuantity = 95,
                ReservedQuantity = 10,
                MinimumStockLevel = 15,
                UnitCost = 42.0m
            }
        );
    }
}
