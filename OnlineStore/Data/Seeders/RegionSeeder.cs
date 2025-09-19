namespace OnlineStore.Data.Seeders;

using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;

public static class RegionSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // ===== Countries =====
        modelBuilder.Entity<Country>().HasData(
            new Country { Id = 1, Code = "AE", PhoneCode = "+971", IsActive = true },
            new Country { Id = 2, Code = "EG", PhoneCode = "+20", IsActive = true }
        );

        // ===== Country Translations =====
        modelBuilder.Entity<CountryTranslation>().HasData(
            // Arabic
            new CountryTranslation { Id = 1, CountryId = 1, LanguageCode = "ar", Name = "الإمارات" },
            new CountryTranslation { Id = 2, CountryId = 2, LanguageCode = "ar", Name = "مصر" },
            // English
            new CountryTranslation { Id = 3, CountryId = 1, LanguageCode = "en", Name = "United Arab Emirates" },
            new CountryTranslation { Id = 4, CountryId = 2, LanguageCode = "en", Name = "Egypt" }
        );

        // ===== States =====
        modelBuilder.Entity<State>().HasData(
            new State { Id = 1, CountryId = 1, Code = "DXB" },
            new State { Id = 2, CountryId = 1, Code = "ABU" },
            new State { Id = 3, CountryId = 2, Code = "CAI" }
        );

        // ===== State Translations =====
        modelBuilder.Entity<StateTranslation>().HasData(
            // Arabic
            new StateTranslation { Id = 1, StateId = 1, LanguageCode = "ar", Name = "دبي" },
            new StateTranslation { Id = 2, StateId = 2, LanguageCode = "ar", Name = "أبو ظبي" },
            new StateTranslation { Id = 3, StateId = 3, LanguageCode = "ar", Name = "القاهرة" },
            // English
            new StateTranslation { Id = 4, StateId = 1, LanguageCode = "en", Name = "Dubai" },
            new StateTranslation { Id = 5, StateId = 2, LanguageCode = "en", Name = "Abu Dhabi" },
            new StateTranslation { Id = 6, StateId = 3, LanguageCode = "en", Name = "Cairo" }
        );

        // ===== Cities =====
        modelBuilder.Entity<City>().HasData(
            new City { Id = 1, StateId = 1, Name = "Downtown Dubai" },
            new City { Id = 2, StateId = 1, Name = "Marina" },
            new City { Id = 3, StateId = 3, Name = "Nasr City" }
        );

        // ===== City Translations =====
        modelBuilder.Entity<CityTranslation>().HasData(
            // Arabic
            new CityTranslation { Id = 1, CityId = 1, LanguageCode = "ar", Name = "وسط مدينة دبي" },
            new CityTranslation { Id = 2, CityId = 2, LanguageCode = "ar", Name = "مارينا" },
            new CityTranslation { Id = 3, CityId = 3, LanguageCode = "ar", Name = "مدينة نصر" },
            // English
            new CityTranslation { Id = 4, CityId = 1, LanguageCode = "en", Name = "Downtown Dubai" },
            new CityTranslation { Id = 5, CityId = 2, LanguageCode = "en", Name = "Marina" },
            new CityTranslation { Id = 6, CityId = 3, LanguageCode = "en", Name = "Nasr City" }
        );
    }
}
