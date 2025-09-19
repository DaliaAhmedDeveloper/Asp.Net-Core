using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using OnlineStore.Data.Configurations;
using OnlineStore.Data.Seeders;
using System.Linq.Expressions;

// this is called fluent Api : its using to add configuration on models like add constraint
// NULL DEFAULT Forign Key Primary key , Required , Unique , also table mapping also  , relation and specify on delete action

//You want to create complex relationships between tables (one-to-many, many-to-many).
// You don't want to write code for each property.
// You want all settings to be in one place.
// You want to control the name of a table or column within the database.

public class AppDbContext : DbContext
{
    // database sets
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<AttributeValue> AttributeValues { get; set; }
    public DbSet<AttributeValueTranslation> AttributeValueTranslations { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<LogTranslation> LogTranslations { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<NotificationTranslation> NotificationTranslations { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentTranslation> PaymentTranslations { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductAttribute> ProductAttributes { get; set; }
    public DbSet<ProductAttributeTranslation> ProductAttributeTranslations { get; set; }
    public DbSet<ProductTranslation> ProductTranslations { get; set; }
    public DbSet<ProductVariant> Variants { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<ShippingMethod> ShippingMethods { get; set; }
    public DbSet<ShippingMethodTranslation> ShippingMethodTranslations { get; set; }
    public DbSet<SiteSetting> SiteSettings { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TagTranslation> TagTranslations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Wallet> Wallet { get; set; }
    public DbSet<WalletTransaction> WalletTransactions { get; set; }
    public DbSet<VariantAttributeValue> VariantAttributeValues { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RoleTranslation> RoleTranslations { get; set; }
    public DbSet<PermissionTranslation> PermissionTranslations { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Wishlist> Wishlist { get; set; }
    public DbSet<UserPoint> UserPoints { get; set; }
    public DbSet<OrderTracking> OrderTracking { get; set; }
    public DbSet<ReturnTracking> ReturnTracking { get; set; }
    public DbSet<Return> Returns { get; set; }
    public DbSet<ReturnItem> ReturnItems { get; set; }
    public DbSet<ReturnAttachment> ReturnAttachments { get; set; }
    public DbSet<SupportTicket> SupportTickets { get; set; }
    public DbSet<TicketMessage> TicketMessages { get; set; }
    public DbSet<PasswordReset> PasswordResets { get; set; }
    public DbSet<Stock> Stock { get; set; }
    public DbSet<StockMovement> StockMovements { get; set; }
    public DbSet<CouponUser> couponUser { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<AppSetting> Appsettings { get; set; }
    public DbSet<ReviewAttachment> ReviewAttachements { get; set; }
    public DbSet<FailedTask> FailedTasks { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    // Helper method to create query filter dynamically at run time using reflection
    // we need to generate expression like this entity => !entity.IsDeleted
    // so we need parameter , property , condition , then return lambda
    private static LambdaExpression ConvertFilterExpression(Type entityType)
    {
        // create parameter called e of its Type
        var parameter = Expression.Parameter(entityType, "e");
        /* create property for paramter called IsDeleted but its type is not string its expression 
        why we cant use IsDeleted string direct instead of  nameof(BaseEntity.IsDeleted)
        its ok it will work but its not the best way , why ?
        nameof(BaseEntity.IsDeleted) here if u change the name of property later compiler will show an error here
        this property is not correct , but if u hard codded it as text not compiler error so will through exception in run time */
        var prop = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));

        /*
        here also we cant use false direct ?? the answer is NOOO , why ??
        because of this method Expression.Equal behaviour is working with 2 expressions 
        */
        var condition = Expression.Equal(prop, Expression.Constant(false)); // IsDeleted = false
        var lambda = Expression.Lambda(condition, parameter); // generate expression
        return lambda;
        // will return something like entity => !entity.IsDeleted
    }

    // for soft delete
    // override SaveChangesASync method from DbContent class
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // fetch all objects inherit from BaseEntity class
        var entries = ChangeTracker.Entries<BaseEntity>();

        // loop through objects (objects here are the models)
        foreach (var entry in entries)
        {
            switch (entry.State) // entry has state property which is return the state of this database object
            {
                // if its first time it will be added (EntityState.Added)
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;

                // if its not the first time it will be Modified (EntityState.Modified)
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;

                    // // if its deleted ,, no need here because i will use hard delete also 
                    // case EntityState.Deleted:
                    //     entry.State = EntityState.Modified;
                    //     entry.Entity.IsDeleted = true;
                    //     break;
            }
        }

        //Run the base EF Core logic to save changes to the database, and give me back how many changes were made.”
        return await base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Apply IsDeleted = false globally
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()) // here loop to all models objects
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType)) // here check if inherit from BaseEntity class
            {
                /* 
                clr : stands for common language runtime
                generate query to exclude records where IsDeleted field has value
                entityType.ClrType : this is the type of object like User but compiler does not understand it 
                so we have to use expression tree for that so will be understandable at run time 
                */
                modelBuilder.Entity(entityType.ClrType)
                    .HasQueryFilter(ConvertFilterExpression(entityType.ClrType));
            }
        }

        // properties configuration , Relationships & Delete Behavior

        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new AttributeValueConfiguration());
        modelBuilder.ApplyConfiguration(new AttributeValueTranslationConfiguration());
        modelBuilder.ApplyConfiguration(new CartConfiguration());
        modelBuilder.ApplyConfiguration(new CartItemConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryTranslationConfiguration());
        modelBuilder.ApplyConfiguration(new CouponConfiguration());
        modelBuilder.ApplyConfiguration(new CouponTranslationConfiguration());
        modelBuilder.ApplyConfiguration(new LogConfiguration());
        modelBuilder.ApplyConfiguration(new LogTranslationConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationTranslationConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentTranslationConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new ProductAttributeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductAttributeTranslationConfiguration());
        modelBuilder.ApplyConfiguration(new ProductTranslationConfiguration());
        modelBuilder.ApplyConfiguration(new ProductVariantConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewConfiguration());
        modelBuilder.ApplyConfiguration(new ShippingMethodConfiguration());
        modelBuilder.ApplyConfiguration(new ShippingMethodTranslationConfiguration());
        modelBuilder.ApplyConfiguration(new SiteSettingConfiguration());
        modelBuilder.ApplyConfiguration(new TagConfiguration());
        modelBuilder.ApplyConfiguration(new TagTranslationConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new VariantAttributeValueConfiguration());
        modelBuilder.ApplyConfiguration(new WalletConfiguration());
        modelBuilder.ApplyConfiguration(new WalletTransactionConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new RoleTranslationConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConTranslationfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        modelBuilder.ApplyConfiguration(new WishlistConfiguration());
        modelBuilder.ApplyConfiguration(new OrderTrackingConfiguration());
        modelBuilder.ApplyConfiguration(new ReturnTrackingConfiguration());
        modelBuilder.ApplyConfiguration(new UserPointConfiguration());
        modelBuilder.ApplyConfiguration(new ReturnConfiguration());
        modelBuilder.ApplyConfiguration(new ReturnItemConfiguration());
        modelBuilder.ApplyConfiguration(new ReturnAttachmentConfiguration());
        modelBuilder.ApplyConfiguration(new PasswordResetConfiguration());
        modelBuilder.ApplyConfiguration(new SupportTicketConfiguration());
        modelBuilder.ApplyConfiguration(new TicketMessageConfiguration());

        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new CountryTranslationConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new CityTranslationConfiguration());
        modelBuilder.ApplyConfiguration(new StateConfiguration());
        modelBuilder.ApplyConfiguration(new StateTranslationConfiguration());
        modelBuilder.ApplyConfiguration(new StockConfiguration());
        modelBuilder.ApplyConfiguration(new StockMovementConfiguration());
        modelBuilder.ApplyConfiguration(new AppSettingConfiguration());
        modelBuilder.ApplyConfiguration(new AppSettingTranslationConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewAttachmentConfiguration());
        modelBuilder.ApplyConfiguration(new FailedTaskConfiguration());

        //seeders
        UserSeeder.Seed(modelBuilder);
        CategorySeeder.Seed(modelBuilder);
        ProductSeeder.Seed(modelBuilder);
        ProductTranslationSeeder.Seed(modelBuilder);
        CategoryProductSeeder.Seed(modelBuilder);

        ProductAttributeSeeder.Seed(modelBuilder);
        AttributeValueSeeder.Seed(modelBuilder);
        ProductVariantSeeder.Seed(modelBuilder);
        VariantAttributeValueSeeder.Seed(modelBuilder);

        TagSeeder.Seed(modelBuilder);
        ProductTagSeeder.Seed(modelBuilder);
        RoleSeeder.Seed(modelBuilder);
        PermissionSeeder.Seed(modelBuilder);
        PermissionRoleSeeder.Seed(modelBuilder);
        UserRoleSeeder.Seed(modelBuilder);
        RegionSeeder.Seed(modelBuilder);
        CouponSeeder.Seed(modelBuilder);
        WarehouseSeeder.Seed(modelBuilder);
        StockSeeder.Seed(modelBuilder);
        AppSettingSeeder.Seed(modelBuilder);
    }

}

