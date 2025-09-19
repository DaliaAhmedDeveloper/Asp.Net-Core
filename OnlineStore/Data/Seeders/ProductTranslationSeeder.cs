namespace OnlineStore.Data.Seeders;

using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;

public static class ProductTranslationSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductTranslation>().HasData(
            // Product 1
            new ProductTranslation { Id = 1, ProductId = 1, LanguageCode = "en", Name = "Smart TV", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", Brand = "Samsung" },
            new ProductTranslation { Id = 2, ProductId = 1, LanguageCode = "ar", Name = "تلفاز ذكي", Description = "لوريم إيبسوم هو نص شكلي في صناعة الطباعة والتنضيد.", Brand = "سامسونج" },

            // Product 2
            new ProductTranslation { Id = 3, ProductId = 2, LanguageCode = "en", Name = "Wireless Headphones", Description = "High-quality wireless sound with noise cancellation.", Brand = "Sony" },
            new ProductTranslation { Id = 4, ProductId = 2, LanguageCode = "ar", Name = "سماعات لاسلكية", Description = "صوت لاسلكي عالي الجودة مع إلغاء الضوضاء.", Brand = "سوني" },

            // Product 3
            new ProductTranslation { Id = 5, ProductId = 3, LanguageCode = "en", Name = "Laptop Pro 15", Description = "Powerful performance with sleek design.", Brand = "Dell" },
            new ProductTranslation { Id = 6, ProductId = 3, LanguageCode = "ar", Name = "حاسوب محمول برو 15", Description = "أداء قوي مع تصميم أنيق.", Brand = "ديل" },

            // Product 4
            new ProductTranslation { Id = 7, ProductId = 4, LanguageCode = "en", Name = "Smartphone X12", Description = "Next-gen smartphone with OLED display.", Brand = "Apple" },
            new ProductTranslation { Id = 8, ProductId = 4, LanguageCode = "ar", Name = "هاتف ذكي X12", Description = "هاتف ذكي من الجيل القادم بشاشة OLED.", Brand = "آبل" },

            // Product 5
            new ProductTranslation { Id = 9, ProductId = 5, LanguageCode = "en", Name = "Gaming Console Z", Description = "Immersive gaming experience.", Brand = "Microsoft" },
            new ProductTranslation { Id = 10, ProductId = 5, LanguageCode = "ar", Name = "جهاز ألعاب Z", Description = "تجربة ألعاب غامرة.", Brand = "مايكروسوفت" },

            // Product 6
            new ProductTranslation { Id = 11, ProductId = 6, LanguageCode = "en", Name = "Bluetooth Speaker", Description = "Portable speaker with deep bass.", Brand = "JBL" },
            new ProductTranslation { Id = 12, ProductId = 6, LanguageCode = "ar", Name = "مكبر صوت بلوتوث", Description = "مكبر صوت محمول مع باس عميق.", Brand = "جي بي إل" },

            // Product 7
            new ProductTranslation { Id = 13, ProductId = 7, LanguageCode = "en", Name = "4K Action Camera", Description = "Capture high-resolution outdoor adventures.", Brand = "GoPro" },
            new ProductTranslation { Id = 14, ProductId = 7, LanguageCode = "ar", Name = "كاميرا أكشن 4K", Description = "التقط مغامرات خارجية عالية الدقة.", Brand = "جو برو" },

            // Product 8
            new ProductTranslation { Id = 15, ProductId = 8, LanguageCode = "en", Name = "Smart Watch S9", Description = "Fitness and health tracking on your wrist.", Brand = "Fitbit" },
            new ProductTranslation { Id = 16, ProductId = 8, LanguageCode = "ar", Name = "ساعة ذكية S9", Description = "تتبع اللياقة والصحة على معصمك.", Brand = "فيتبيت" },

            // Product 9
            new ProductTranslation { Id = 17, ProductId = 9, LanguageCode = "en", Name = "VR Headset", Description = "Experience virtual reality at its best.", Brand = "Meta" },
            new ProductTranslation { Id = 18, ProductId = 9, LanguageCode = "ar", Name = "سماعة الواقع الافتراضي", Description = "اختبر الواقع الافتراضي بأفضل صورة.", Brand = "ميتا" },

            // Product 10
            new ProductTranslation { Id = 19, ProductId = 10, LanguageCode = "en", Name = "Drone with Camera", Description = "Fly and record stunning aerial shots.", Brand = "DJI" },
            new ProductTranslation { Id = 20, ProductId = 10, LanguageCode = "ar", Name = "طائرة بدون طيار مع كاميرا", Description = "حلّق وسجل لقطات جوية مذهلة.", Brand = "دي جي آي" },

            // Product 11
            new ProductTranslation { Id = 21, ProductId = 11, LanguageCode = "en", Name = "E-Reader", Description = "Read thousands of books on the go.", Brand = "Amazon" },
            new ProductTranslation { Id = 22, ProductId = 11, LanguageCode = "ar", Name = "قارئ إلكتروني", Description = "اقرأ آلاف الكتب أثناء التنقل.", Brand = "أمازون" },

            // Product 12
            new ProductTranslation { Id = 23, ProductId = 12, LanguageCode = "en", Name = "Smart Home Hub", Description = "Control all your smart devices easily.", Brand = "Google" },
            new ProductTranslation { Id = 24, ProductId = 12, LanguageCode = "ar", Name = "مركز المنزل الذكي", Description = "تحكم في جميع أجهزتك الذكية بسهولة.", Brand = "جوجل" },

            // Product 13
            new ProductTranslation { Id = 25, ProductId = 13, LanguageCode = "en", Name = "Wireless Router", Description = "Fast, reliable Wi-Fi connection.", Brand = "TP-Link" },
            new ProductTranslation { Id = 26, ProductId = 13, LanguageCode = "ar", Name = "راوتر لاسلكي", Description = "اتصال واي فاي سريع وموثوق.", Brand = "تي بي-لينك" },

            // Product 14
            new ProductTranslation { Id = 27, ProductId = 14, LanguageCode = "en", Name = "Desktop PC", Description = "Powerful workstation for home or office.", Brand = "HP" },
            new ProductTranslation { Id = 28, ProductId = 14, LanguageCode = "ar", Name = "كمبيوتر مكتبي", Description = "محطة عمل قوية للمنزل أو المكتب.", Brand = "إتش بي" },

            // Product 15
            new ProductTranslation { Id = 29, ProductId = 15, LanguageCode = "en", Name = "Portable Hard Drive", Description = "1TB storage on the go.", Brand = "Seagate" },
            new ProductTranslation { Id = 30, ProductId = 15, LanguageCode = "ar", Name = "قرص صلب محمول", Description = "تخزين 1 تيرابايت أثناء التنقل.", Brand = "سيجيت" },

            // Product 16
            new ProductTranslation { Id = 31, ProductId = 16, LanguageCode = "en", Name = "Noise Cancelling Earbuds", Description = "Compact and immersive sound.", Brand = "Bose" },
            new ProductTranslation { Id = 32, ProductId = 16, LanguageCode = "ar", Name = "سماعات أذن عازلة للضوضاء", Description = "صوت مدمج وغامر.", Brand = "بوز" },

            // Product 17
            new ProductTranslation { Id = 33, ProductId = 17, LanguageCode = "en", Name = "Smart Thermostat", Description = "Control your home's temperature remotely.", Brand = "Nest" },
            new ProductTranslation { Id = 34, ProductId = 17, LanguageCode = "ar", Name = "ثرموستات ذكي", Description = "تحكم في درجة حرارة منزلك عن بُعد.", Brand = "نيست" },

            // Product 18
            new ProductTranslation { Id = 35, ProductId = 18, LanguageCode = "en", Name = "Digital Camera", Description = "Perfect for amateur and professional photography.", Brand = "Canon" },
            new ProductTranslation { Id = 36, ProductId = 18, LanguageCode = "ar", Name = "كاميرا رقمية", Description = "مثالية للتصوير الهواة والمحترفين.", Brand = "كانون" },

            // Product 19
            new ProductTranslation { Id = 37, ProductId = 19, LanguageCode = "en", Name = "Tablet Pro", Description = "Large screen tablet with stylus support.", Brand = "Apple" },
            new ProductTranslation { Id = 38, ProductId = 19, LanguageCode = "ar", Name = "تابلت برو", Description = "تابلت بشاشة كبيرة مع دعم القلم.", Brand = "آبل" },

            // Product 20
            new ProductTranslation { Id = 39, ProductId = 20, LanguageCode = "en", Name = "Smart Light Bulbs", Description = "Automate your lighting with your phone.", Brand = "Philips" },
            new ProductTranslation { Id = 40, ProductId = 20, LanguageCode = "ar", Name = "لمبات ذكية", Description = "أتمتة الإضاءة عبر هاتفك.", Brand = "فيلبس" }
        );
    }
}
