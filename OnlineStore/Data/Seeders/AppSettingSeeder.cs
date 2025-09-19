namespace OnlineStore.Data.Seeders;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;

public static class AppSettingSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // Main AppSettings (actual values)
        modelBuilder.Entity<AppSetting>().HasData(
            new AppSetting { Id = 1, Code = "admin_email", Value = "admin@example.com" },
            new AppSetting { Id = 2, Code = "support_email", Value = "support@example.com" },
            new AppSetting { Id = 3, Code = "phone_number", Value = "+971123456789" },
            new AppSetting { Id = 4, Code = "logo", Value = "logo.png" },
            new AppSetting { Id = 5, Code = "app_title", Value = "Online Store" },
            new AppSetting { Id = 6, Code = "app_description", Value = "Best online store in UAE" },
            new AppSetting { Id = 7, Code = "facebook", Value = "https://facebook.com/yourpage" },
            new AppSetting { Id = 8, Code = "whatsapp", Value = "https://wa.me/971123456789" },
            new AppSetting { Id = 9, Code = "instagram", Value = "https://instagram.com/yourpage" },
            new AppSetting { Id = 10, Code = "linkedin", Value = "https://linkedin.com/company/yourpage" },
            new AppSetting { Id = 11, Code = "snapchat", Value = "https://snapchat.com/add/yourpage" },
            new AppSetting { Id = 12, Code = "twitter", Value = "https://twitter.com/yourpage" },
            new AppSetting { Id = 13, Code = "youtube", Value = "https://youtube.com/yourchannel" },
            new AppSetting { Id = 14, Code = "cash_on_delivery_fees", Value = "10.00" },
            new AppSetting { Id = 15, Code = "default_currency", Value = "AED" },
            new AppSetting { Id = 16, Code = "timezone", Value = "Asia/Dubai" }
        );

        // Translations (display names only)
        modelBuilder.Entity<AppSettingTranslation>().HasData(
            // Admin Email
            new AppSettingTranslation { Id = 1, AppSettingId = 1, Key = "Admin Email", LanguageCode = "en" },
            new AppSettingTranslation { Id = 2, AppSettingId = 1, Key = "البريد الإلكتروني للمسؤول", LanguageCode = "ar" },

            // Support Email
            new AppSettingTranslation { Id = 3, AppSettingId = 2, Key = "Support Email", LanguageCode = "en" },
            new AppSettingTranslation { Id = 4, AppSettingId = 2, Key = "البريد الإلكتروني للدعم", LanguageCode = "ar" },

            // Phone Number
            new AppSettingTranslation { Id = 5, AppSettingId = 3, Key = "Phone Number", LanguageCode = "en" },
            new AppSettingTranslation { Id = 6, AppSettingId = 3, Key = "رقم الهاتف", LanguageCode = "ar" },

            // Logo
            new AppSettingTranslation { Id = 7, AppSettingId = 4, Key = "Logo", LanguageCode = "en" },
            new AppSettingTranslation { Id = 8, AppSettingId = 4, Key = "شعار التطبيق", LanguageCode = "ar" },

            // App Title
            new AppSettingTranslation { Id = 9, AppSettingId = 5, Key = "App Title", LanguageCode = "en" },
            new AppSettingTranslation { Id = 10, AppSettingId = 5, Key = "عنوان التطبيق", LanguageCode = "ar" },

            // App Description
            new AppSettingTranslation { Id = 11, AppSettingId = 6, Key = "App Description", LanguageCode = "en" },
            new AppSettingTranslation { Id = 12, AppSettingId = 6, Key = "وصف التطبيق", LanguageCode = "ar" },

            // Facebook
            new AppSettingTranslation { Id = 13, AppSettingId = 7, Key = "Facebook", LanguageCode = "en" },
            new AppSettingTranslation { Id = 14, AppSettingId = 7, Key = "فيس بوك", LanguageCode = "ar" },

            // WhatsApp
            new AppSettingTranslation { Id = 15, AppSettingId = 8, Key = "WhatsApp", LanguageCode = "en" },
            new AppSettingTranslation { Id = 16, AppSettingId = 8, Key = "واتساب", LanguageCode = "ar" },

            // Instagram
            new AppSettingTranslation { Id = 17, AppSettingId = 9, Key = "Instagram", LanguageCode = "en" },
            new AppSettingTranslation { Id = 18, AppSettingId = 9, Key = "انستغرام", LanguageCode = "ar" },

            // LinkedIn
            new AppSettingTranslation { Id = 19, AppSettingId = 10, Key = "LinkedIn", LanguageCode = "en" },
            new AppSettingTranslation { Id = 20, AppSettingId = 10, Key = "لينكدإن", LanguageCode = "ar" },

            // Snapchat
            new AppSettingTranslation { Id = 21, AppSettingId = 11, Key = "Snapchat", LanguageCode = "en" },
            new AppSettingTranslation { Id = 22, AppSettingId = 11, Key = "سناب شات", LanguageCode = "ar" },

            // Twitter
            new AppSettingTranslation { Id = 23, AppSettingId = 12, Key = "Twitter", LanguageCode = "en" },
            new AppSettingTranslation { Id = 24, AppSettingId = 12, Key = "تويتر", LanguageCode = "ar" },

            // YouTube
            new AppSettingTranslation { Id = 25, AppSettingId = 13, Key = "YouTube", LanguageCode = "en" },
            new AppSettingTranslation { Id = 26, AppSettingId = 13, Key = "يوتيوب", LanguageCode = "ar" },

            // Cash on Delivery Fees
            new AppSettingTranslation { Id = 27, AppSettingId = 14, Key = "Cash on Delivery Fees", LanguageCode = "en" },
            new AppSettingTranslation { Id = 28, AppSettingId = 14, Key = "رسوم الدفع عند الاستلام", LanguageCode = "ar" },

            // Default Currency
            new AppSettingTranslation { Id = 29, AppSettingId = 15, Key = "Default Currency", LanguageCode = "en" },
            new AppSettingTranslation { Id = 30, AppSettingId = 15, Key = "العملة الافتراضية", LanguageCode = "ar" },

            // Timezone
            new AppSettingTranslation { Id = 31, AppSettingId = 16, Key = "Timezone", LanguageCode = "en" },
            new AppSettingTranslation { Id = 32, AppSettingId = 16, Key = "المنطقة الزمنية", LanguageCode = "ar" }
        );
    }
}
