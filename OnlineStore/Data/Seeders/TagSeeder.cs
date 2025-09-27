namespace OnlineStore.Data.Seeders;

using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;

public static class TagSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tag>().HasData(
            new Tag { Id = 1, Code = "untaged" },
            new Tag { Id = 2, Code = "Smart" },
            new Tag { Id = 3, Code = "Electricity" },
            new Tag { Id = 4, Code = "White" },
            new Tag { Id = 5, Code = "Large" },
            new Tag { Id = 6, Code = "Good Deal" },
            new Tag { Id = 7, Code = "Electronics" }
        );

        // translations

        modelBuilder.Entity<TagTranslation>().HasData(
            // Arabic Translations
            new TagTranslation { Id = 1, TagId = 1, LanguageCode = "ar", Name = "إلكترونيات" },
            new TagTranslation { Id = 2, TagId = 2, LanguageCode = "ar", Name = "ذكي" },
            new TagTranslation { Id = 3, TagId = 3, LanguageCode = "ar", Name = "كهرباء" },
            new TagTranslation { Id = 4, TagId = 4, LanguageCode = "ar", Name = "أبيض" },
            new TagTranslation { Id = 5, TagId = 5, LanguageCode = "ar", Name = "كبير" },
            new TagTranslation { Id = 6, TagId = 6, LanguageCode = "ar", Name = "صفقة جيدة" },

            // English Translations
            new TagTranslation { Id = 7, TagId = 1, LanguageCode = "en", Name = "Electronics" },
            new TagTranslation { Id = 8, TagId = 2, LanguageCode = "en", Name = "Smart" },
            new TagTranslation { Id = 9, TagId = 3, LanguageCode = "en", Name = "Electricity" },
            new TagTranslation { Id = 10, TagId = 4, LanguageCode = "en", Name = "White" },
            new TagTranslation { Id = 11, TagId = 5, LanguageCode = "en", Name = "Large" },
            new TagTranslation { Id = 12, TagId = 6, LanguageCode = "en", Name = "Good Deal" }
        );

    }
}
