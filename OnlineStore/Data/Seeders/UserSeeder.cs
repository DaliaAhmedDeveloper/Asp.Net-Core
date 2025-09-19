namespace OnlineStore.Data.Seeders;

using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Helpers;
using OnlineStore.Models.Enums;

public static class UserSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
             new User
            {
                Id = 1,
                FullName = "Dalia Ali",
                Provider = null,
                PasswordHash = "AQAAAAIAAYagAAAAEFiMjDe70MmfsT4pSIO2bsgI3QYt6fnyGNRbkpTVi8e6vk+TzkhNNq6BUHT4P1p2Tw==",
                Email = "dalia@effortz.co",
                IsActive = true,
                PhoneNumber = "98988787",
                UserType = UserType.Admin,
                CountryId = 1, // UAE
                StateId = 1,   // Dubai
                CityId = 1,    // Downtown Dubai
            },
            new User
            {
                Id = 2,
                FullName = "Dalia Ahmed",
                Provider = null,
                PasswordHash = "AQAAAAIAAYagAAAAEFiMjDe70MmfsT4pSIO2bsgI3QYt6fnyGNRbkpTVi8e6vk+TzkhNNq6BUHT4P1p2Tw==",
                Email = "dalia@gmail.com",
                IsActive = true,
                PhoneNumber = "98988707",
                UserType = UserType.User,
                CountryId = 2, // Egypt
                StateId = 3,   // Cairo
                CityId = 3,    // Nasr City
            }
        );
    }
}
