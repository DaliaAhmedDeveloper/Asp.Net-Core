namespace OnlineStore.Data.Seeders;

using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;
public static class AttributeValueSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AttributeValue>().HasData(
            new AttributeValue { Id = 1, Code = "Red", AttributeId = 1 },
            new AttributeValue { Id = 2, Code = "Black", AttributeId = 1 },
            new AttributeValue { Id = 3, Code = "Green", AttributeId = 1 },
            new AttributeValue { Id = 4, Code = "XL", AttributeId = 2 },
            new AttributeValue { Id = 5, Code = "XXL", AttributeId = 2 },
            new AttributeValue { Id = 6, Code = "XXXL", AttributeId = 2 },
            new AttributeValue { Id = 7, Code = "ZARA", AttributeId = 3 },
            new AttributeValue { Id = 8, Code = "MAX", AttributeId = 3 },
            new AttributeValue { Id = 9, Code = "FERRARI", AttributeId = 4 },
            new AttributeValue { Id = 10, Code = "TOYOTA", AttributeId = 4 }
        );
        
        modelBuilder.Entity<AttributeValueTranslation>().HasData(
            // Arabic Translations
            new AttributeValueTranslation { Id = 1, AttributeValueId = 1, LanguageCode = "ar", Name = "أحمر" },
            new AttributeValueTranslation { Id = 2, AttributeValueId = 2, LanguageCode = "ar", Name = "أسود" },
            new AttributeValueTranslation { Id = 3, AttributeValueId = 3, LanguageCode = "ar", Name = "أخضر" },
            new AttributeValueTranslation { Id = 4, AttributeValueId = 4, LanguageCode = "ar", Name = "إكس لارج" },
            new AttributeValueTranslation { Id = 5, AttributeValueId = 5, LanguageCode = "ar", Name = "إكس إكس لارج" },
            new AttributeValueTranslation { Id = 6, AttributeValueId = 6, LanguageCode = "ar", Name = "إكس إكس إكس لارج" },
            new AttributeValueTranslation { Id = 7, AttributeValueId = 7, LanguageCode = "ar", Name = "زارا" },
            new AttributeValueTranslation { Id = 8, AttributeValueId = 8, LanguageCode = "ar", Name = "ماكس" },
            new AttributeValueTranslation { Id = 9, AttributeValueId = 9, LanguageCode = "ar", Name = "فيراري" },
            new AttributeValueTranslation { Id = 10, AttributeValueId = 10, LanguageCode = "ar", Name = "تويوتا" },

            // English Translations
            new AttributeValueTranslation { Id = 11, AttributeValueId = 1, LanguageCode = "en", Name = "Red" },
            new AttributeValueTranslation { Id = 12, AttributeValueId = 2, LanguageCode = "en", Name = "Black" },
            new AttributeValueTranslation { Id = 13, AttributeValueId = 3, LanguageCode = "en", Name = "Green" },
            new AttributeValueTranslation { Id = 14, AttributeValueId = 4, LanguageCode = "en", Name = "XL" },
            new AttributeValueTranslation { Id = 15, AttributeValueId = 5, LanguageCode = "en", Name = "XXL" },
            new AttributeValueTranslation { Id = 16, AttributeValueId = 6, LanguageCode = "en", Name = "XXXL" },
            new AttributeValueTranslation { Id = 17, AttributeValueId = 7, LanguageCode = "en", Name = "ZARA" },
            new AttributeValueTranslation { Id = 18, AttributeValueId = 8, LanguageCode = "en", Name = "MAX" },
            new AttributeValueTranslation { Id = 19, AttributeValueId = 9, LanguageCode = "en", Name = "FERRARI" },
            new AttributeValueTranslation { Id = 20, AttributeValueId = 10, LanguageCode = "en", Name = "TOYOTA" }
        );

    }
}
