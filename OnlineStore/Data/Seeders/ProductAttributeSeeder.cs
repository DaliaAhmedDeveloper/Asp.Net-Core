namespace OnlineStore.Data.Seeders;

using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;
public static class ProductAttributeSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductAttribute>().HasData(
            new ProductAttribute { Id = 1, Code = "Color" },
            new ProductAttribute { Id = 2, Code = "Size" },
            new ProductAttribute { Id = 3, Code = "Brand" },
            new ProductAttribute { Id = 4, Code = "Model" }
        );

        modelBuilder.Entity<ProductAttributeTranslation>().HasData(
            // Arabic Translations
            new ProductAttributeTranslation { Id = 1, ProductAttributeId = 1, LanguageCode = "ar", Name = "اللون"  },
            new ProductAttributeTranslation { Id = 2, ProductAttributeId = 2, LanguageCode = "ar", Name = "الحجم" },
            new ProductAttributeTranslation { Id = 3, ProductAttributeId = 3, LanguageCode = "ar", Name = "العلامة التجارية" },
            new ProductAttributeTranslation { Id = 4, ProductAttributeId = 4, LanguageCode = "ar", Name = "الموديل" },

            // English Translations
            new ProductAttributeTranslation { Id = 5, ProductAttributeId = 1, LanguageCode = "en", Name = "Color" },
            new ProductAttributeTranslation { Id = 6, ProductAttributeId = 2, LanguageCode = "en", Name = "Size" },
            new ProductAttributeTranslation { Id = 7, ProductAttributeId = 3, LanguageCode = "en", Name = "Brand" },
            new ProductAttributeTranslation { Id = 8, ProductAttributeId = 4, LanguageCode = "en", Name = "Model" }
        );

    }
}
