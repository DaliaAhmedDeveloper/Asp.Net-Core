namespace OnlineStore.Data.Seeders;
using Microsoft.EntityFrameworkCore;
public static class PermissionSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        int idCounter = 1;

        Permission CreatePermission(string slug) => new Permission { Id = idCounter++, Slug = slug };

        var permissions = new List<Permission>
        {
            // User
            CreatePermission("user.add"),
            CreatePermission("user.update"),
            CreatePermission("user.list"),
            CreatePermission("user.show"),
            CreatePermission("user.delete"),
            
             // tag
            CreatePermission("tag.add"),
            CreatePermission("tag.update"),
            CreatePermission("tag.list"),
            CreatePermission("tag.show"),
            CreatePermission("tag.delete"),
            // Category
            CreatePermission("category.add"),
            CreatePermission("category.update"),
            CreatePermission("category.list"),
            CreatePermission("category.show"),
            CreatePermission("category.delete"),

            // Country
            CreatePermission("country.add"),
            CreatePermission("country.update"),
            CreatePermission("country.list"),
            CreatePermission("country.show"),
            CreatePermission("country.delete"),

            // State
            CreatePermission("state.add"),
            CreatePermission("state.update"),
            CreatePermission("state.list"),
            CreatePermission("state.show"),
            CreatePermission("state.delete"),

            // City
            CreatePermission("city.add"),
            CreatePermission("city.update"),
            CreatePermission("city.list"),
            CreatePermission("city.show"),
            CreatePermission("city.delete"),

            // Logs
            CreatePermission("logs.list"),
            CreatePermission("logs.show"),

            // Notification
            CreatePermission("notification.list"),
            CreatePermission("notification.show"),
            CreatePermission("notification.delete"),
            CreatePermission("notification.deleteAll"),

            // Settings
            CreatePermission("settings.list"),
            CreatePermission("settings.show"),
            CreatePermission("settings.update"),

            // Review
            CreatePermission("review.list"),
            CreatePermission("review.show"),
            CreatePermission("review.accept"),

            // Attribute
            CreatePermission("attribute.add"),
            CreatePermission("attribute.update"),
            CreatePermission("attribute.list"),
            CreatePermission("attribute.show"),
            CreatePermission("attribute.delete"),

            // AttributeValue
            CreatePermission("attributeValue.add"),
            CreatePermission("attributeValue.update"),
            CreatePermission("attributeValue.list"),
            CreatePermission("attributeValue.show"),
            CreatePermission("attributeValue.delete"),

            // Role
            CreatePermission("role.add"),
            CreatePermission("role.update"),
            CreatePermission("role.list"),
            CreatePermission("role.show"),
            CreatePermission("role.delete"),

            // Product
            CreatePermission("product.add"),
            CreatePermission("product.update"),
            CreatePermission("product.list"),
            CreatePermission("product.show"),
            CreatePermission("product.delete"),

            // Order
            CreatePermission("order.list"),
            CreatePermission("order.show"),
            CreatePermission("order.update"),
            CreatePermission("order.delete"),

            // Return
            CreatePermission("return.list"),
            CreatePermission("return.show"),
            CreatePermission("return.update"),
            CreatePermission("return.delete"),

            // Coupon
            CreatePermission("coupon.add"),
            CreatePermission("coupon.update"),
            CreatePermission("coupon.list"),
            CreatePermission("coupon.show"),
            CreatePermission("coupon.delete"),

            // Warehouse
            CreatePermission("warehouse.add"),
            CreatePermission("warehouse.update"),
            CreatePermission("warehouse.list"),
            CreatePermission("warehouse.show"),
            CreatePermission("warehouse.delete"),

            // Support Ticket
            CreatePermission("supportTicket.list"),
            CreatePermission("supportTicket.show"),

            // Ticket Message
            CreatePermission("ticketMessage.add"),
            CreatePermission("ticketMessage.list"),
            CreatePermission("ticketMessage.show"),
        };

        modelBuilder.Entity<Permission>().HasData(permissions);

        int transId = 1;
        var translations = new List<PermissionTranslation>();

        foreach (var perm in permissions)
        {
            string arName = perm.Slug switch
            {
                // User
                "user.add" => "إضافة مستخدم",
                "user.update" => "تعديل مستخدم",
                "user.list" => "عرض المستخدمين",
                "user.show" => "عرض مستخدم",
                "user.delete" => "حذف مستخدم",

                // Tag
                "tag.add"    => "إضافة تاج",
                "tag.update" => "تعديل تاج",
                "tag.list"   => "عرض التاجات",
                "tag.show"   => "عرض تاج",
                "tag.delete" => "حذف تاج",


                // Category
                "category.add" => "إضافة تصنيف",
                "category.update" => "تعديل تصنيف",
                "category.list" => "عرض التصنيفات",
                "category.show" => "عرض تصنيف",
                "category.delete" => "حذف تصنيف",

                // Country
                "country.add" => "إضافة دولة",
                "country.update" => "تعديل دولة",
                "country.list" => "عرض الدول",
                "country.show" => "عرض دولة",
                "country.delete" => "حذف دولة",

                // State
                "state.add" => "إضافة ولاية",
                "state.update" => "تعديل ولاية",
                "state.list" => "عرض الولايات",
                "state.show" => "عرض ولاية",
                "state.delete" => "حذف ولاية",

                // City
                "city.add" => "إضافة مدينة",
                "city.update" => "تعديل مدينة",
                "city.list" => "عرض المدن",
                "city.show" => "عرض مدينة",
                "city.delete" => "حذف مدينة",

                // Logs
                "logs.list" => "عرض السجلات",
                "logs.show" => "عرض سجل",

                // Notification
                "notification.list" => "عرض الإشعارات",
                "notification.show" => "عرض إشعار",
                "notification.delete" => "حذف إشعار",
                "notification.deleteAll" => "حذف كل الإشعارات",

                // Settings
                "settings.list" => "عرض الإعدادات",
                "settings.show" => "عرض إعداد",
                "settings.update" => "تعديل الإعدادات",

                // Review
                "review.list" => "عرض التقييمات",
                "review.show" => "عرض تقييم",
                "review.accept" => "قبول التقييم",

                // Attribute
                "attribute.add" => "إضافة خاصية",
                "attribute.update" => "تعديل خاصية",
                "attribute.list" => "عرض الخصائص",
                "attribute.show" => "عرض خاصية",
                "attribute.delete" => "حذف خاصية",

                // AttributeValue
                "attributeValue.add" => "إضافة قيمة خاصية",
                "attributeValue.update" => "تعديل قيمة خاصية",
                "attributeValue.list" => "عرض قيم الخصائص",
                "attributeValue.show" => "عرض قيمة خاصية",
                "attributeValue.delete" => "حذف قيمة خاصية",

                // Role
                "role.add" => "إضافة دور",
                "role.update" => "تعديل دور",
                "role.list" => "عرض الأدوار",
                "role.show" => "عرض دور",
                "role.delete" => "حذف دور",

                // Product
                "product.add" => "إضافة منتج",
                "product.update" => "تعديل منتج",
                "product.list" => "عرض المنتجات",
                "product.show" => "عرض منتج",
                "product.delete" => "حذف منتج",

                // Order
                "order.list" => "عرض الطلبات",
                "order.show" => "عرض طلب",
                "order.update" => "تعديل الطلب",
                "order.delete" => "حذف الطلب",

                // Return
                "return.list" => "عرض المرتجعات",
                "return.show" => "عرض مرتجع",
                "return.update" => "تعديل المرتجع",
                "return.delete" => "حذف المرتجع",

                // Coupon
                "coupon.add" => "إضافة كوبون",
                "coupon.update" => "تعديل كوبون",
                "coupon.list" => "عرض الكوبونات",
                "coupon.show" => "عرض كوبون",
                "coupon.delete" => "حذف كوبون",

                // Warehouse
                "warehouse.add" => "إضافة مستودع",
                "warehouse.update" => "تعديل مستودع",
                "warehouse.list" => "عرض المستودعات",
                "warehouse.show" => "عرض مستودع",
                "warehouse.delete" => "حذف مستودع",

                // Support Ticket
                "supportTicket.list" => "عرض التذاكر",
                "supportTicket.show" => "عرض تذكرة",

                // Ticket Message
                "ticketMessage.add" => "إضافة رسالة تذكرة",
                "ticketMessage.list" => "عرض رسائل التذاكر",
                "ticketMessage.show" => "عرض رسالة تذكرة",

                _ => perm.Slug
            };

            string enName = perm.Slug switch
            {
                // User
                "user.add" => "Add User",
                "user.update" => "Update User",
                "user.list" => "List Users",
                "user.show" => "Show User",
                "user.delete" => "Delete User",

                // Tag
                "tag.add" => "Add Tag",
                "tag.update" => "Update Tag",
                "tag.list" => "List Tags",
                "tag.show" => "Show Tag",
                "tag.delete" => "Delete Tag",


                // Category
                "category.add" => "Add Category",
                "category.update" => "Update Category",
                "category.list" => "List Categories",
                "category.show" => "Show Category",
                "category.delete" => "Delete Category",

                // Country
                "country.add" => "Add Country",
                "country.update" => "Update Country",
                "country.list" => "List Countries",
                "country.show" => "Show Country",
                "country.delete" => "Delete Country",

                // State
                "state.add" => "Add State",
                "state.update" => "Update State",
                "state.list" => "List States",
                "state.show" => "Show State",
                "state.delete" => "Delete State",

                // City
                "city.add" => "Add City",
                "city.update" => "Update City",
                "city.list" => "List Cities",
                "city.show" => "Show City",
                "city.delete" => "Delete City",

                // Logs
                "logs.list" => "List Logs",
                "logs.show" => "Show Log",

                // Notification
                "notification.list" => "List Notifications",
                "notification.show" => "Show Notification",
                "notification.delete" => "Delete Notification",
                "notification.deleteAll" => "Delete All Notifications",

                // Settings
                "settings.list" => "List Settings",
                "settings.show" => "Show Setting",
                "settings.update" => "Update Settings",

                // Review
                "review.list" => "List Reviews",
                "review.show" => "Show Review",
                "review.accept" => "Accept Review",

                // Attribute
                "attribute.add" => "Add Attribute",
                "attribute.update" => "Update Attribute",
                "attribute.list" => "List Attributes",
                "attribute.show" => "Show Attribute",
                "attribute.delete" => "Delete Attribute",

                // AttributeValue
                "attributeValue.add" => "Add Attribute Value",
                "attributeValue.update" => "Update Attribute Value",
                "attributeValue.list" => "List Attribute Values",
                "attributeValue.show" => "Show Attribute Value",
                "attributeValue.delete" => "Delete Attribute Value",

                // Role
                "role.add" => "Add Role",
                "role.update" => "Update Role",
                "role.list" => "List Roles",
                "role.show" => "Show Role",
                "role.delete" => "Delete Role",

                // Product
                "product.add" => "Add Product",
                "product.update" => "Update Product",
                "product.list" => "List Products",
                "product.show" => "Show Product",
                "product.delete" => "Delete Product",

                // Order
                "order.list" => "List Orders",
                "order.show" => "Show Order",
                "order.update" => "Update Order",
                "order.delete" => "Delete Order",

                // Return
                "return.list" => "List Returns",
                "return.show" => "Show Return",
                "return.update" => "Update Return",
                "return.delete" => "Delete Return",

                // Coupon
                "coupon.add" => "Add Coupon",
                "coupon.update" => "Update Coupon",
                "coupon.list" => "List Coupons",
                "coupon.show" => "Show Coupon",
                "coupon.delete" => "Delete Coupon",

                // Warehouse
                "warehouse.add" => "Add Warehouse",
                "warehouse.update" => "Update Warehouse",
                "warehouse.list" => "List Warehouses",
                "warehouse.show" => "Show Warehouse",
                "warehouse.delete" => "Delete Warehouse",

                // Support Ticket
                "supportTicket.list" => "List Support Tickets",
                "supportTicket.show" => "Show Support Ticket",

                // Ticket Message
                "ticketMessage.add" => "Add Ticket Message",
                "ticketMessage.list" => "List Ticket Messages",
                "ticketMessage.show" => "Show Ticket Message",

                _ => perm.Slug
            };

            // Arabic
            translations.Add(new PermissionTranslation
            {
                Id = transId++,
                PermissionId = perm.Id,
                LanguageCode = "ar",
                Name = arName,
                Description = arName
            });

            // English
            translations.Add(new PermissionTranslation
            {
                Id = transId++,
                PermissionId = perm.Id,
                LanguageCode = "en",
                Name = enName,
                Description = enName
            });
        }

        modelBuilder.Entity<PermissionTranslation>().HasData(translations);
    }
}