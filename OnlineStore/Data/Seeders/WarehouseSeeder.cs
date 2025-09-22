namespace OnlineStore.Data.Seeders;

using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;

public static class WarehouseSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Warehouse>().HasData(
            new Warehouse
            {
                Id = 1,
                Code = "WH001",
                Name = "Main Warehouse",
                Address = "123 Business Street",
                City = "Downtown Dubai",
                State = "Dubai",
                Country = "United Arab Emirates",
                ZipCode = "00000",
                Phone = "+971 50 123 4567",
                Email = "warehouse@effortz.co",
                IsActive = true,
                IsDefault = true
            },
            new Warehouse
            {
                Id = 2,
                Code = "WH002",
                Name = "Second Warehouse",
                Address = "123 Business Street",
                City = "Downtown Dubai",
                State = "Dubai",
                Country = "United Arab Emirates",
                ZipCode = "00000",
                Phone = "+971 50 123 4567",
                Email = "warehouse@effortz.co",
                IsActive = true,
                IsDefault = true
            }
        );
    }
}
