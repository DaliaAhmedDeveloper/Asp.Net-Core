# What is DbContext?

Think of DbContext as the main bridge between your C# code and the database. It is the class that EF Core uses to query and save data.

- It represents a session with the database.
- Contains DbSet properties which represent tables in your database.
- Handles tracking changes, saving data, relationships, and querying.

```csharp 
public class AppDbContext : DbContext // create your class which inheret from DbContext class 
{
    // constractor which inject DbContextOptions class of  AppDbContext 
    // its responsible for Connection string , Database provider 
    // Any other EF Core options (like lazy loading, logging)
    // : base(options) Calls the base DbContext constructor with these options.
    // EF Core requires this to initialize its internal behavior.
    //{ } Empty body → nothing else to do in constructor yet.
    // You can add custom logic here if needed (rarely).

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 
}
```

# What you can do inside a DbContext class

The DbContext class is very powerful. Here are the main things you can do:

1.  Define DbSets (tables)
    ```csharp 
    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    ```
    Each DbSet is a table in your database.
    You can query, add, update, delete records using these DbSets.

2. Configure models using Fluent API
   Inside OnModelCreating:

    ```csharp 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<Course>()
            .HasOne(c => c.Teacher)
            .WithMany()
            .HasForeignKey(c => c.TeacherId);
    }
    ```
    Relationships, keys, constraints, table names, column types, default values, indexes, etc.

3. Override SaveChanges / SaveChangesAsync

    You can add custom logic before or after saving changes, like:
    ```csharp 
    public override int SaveChanges()
    {
        // Auto-set CreatedAt/UpdatedAt timestamps
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is BaseEntity baseEntity)
            {
                if (entry.State == EntityState.Added)
                    baseEntity.CreatedAt = DateTime.UtcNow;
                if (entry.State == EntityState.Modified)
                    baseEntity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChanges();
    }
    ```
4. Access ChangeTracker

    ```csharp 
    var addedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
    ```

    Check what entities are added, modified, deleted before saving.
    Useful for logging, auditing, or triggers.

5. Execute Raw SQL queries

    ```csharp 
    var courses = await Courses.FromSqlRaw("SELECT * FROM Courses WHERE Price > 100").ToListAsync();
    ```
    Useful if you want custom SQL while still using EF Core.

6. Transactions

    ```csharp 
    using var transaction = await Database.BeginTransactionAsync();
    try
    {
        // multiple database operations
        await SaveChangesAsync();
        await transaction.CommitAsync();
    }
    catch
    {
        await transaction.RollbackAsync();
    }
    ```
    Handle multiple operations as a single atomic transaction.

7. Database related properties

    ```csharp 
    Database.EnsureCreated(); // create DB if it doesn’t exist
    Database.Migrate();       // apply migrations
    Database.CanConnect();    // check DB connection
    ```

8. Configure behavior globally

    Inside OnConfiguring (optional, mostly for local testing):
    ```csharp 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("YourConnectionString");
        }
    }
    ```

## You have install EF packages :

dotnet add package Microsoft.EntityFrameworkCore -> core EF Package
dotnet add package Microsoft.EntityFrameworkCore.SqlServer -> SQL Server Provider
dotnet add package Microsoft.EntityFrameworkCore.Tools  -> for migrations


# What is EF?

EF stands for Entity Framework.
It’s an Object-Relational Mapper (ORM) for .NET.
An ORM lets you work with a database using C# classes instead of SQL queries.
In other words, it maps your C# objects (entities) to database tables.

```csharp
var student = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == "test@example.com");
```
## What happens here: ##
dbContext.Users → your Users table (DbSet).
FirstOrDefaultAsync comes from LINQ and EF translates it into a SQL query

```sql
SELECT TOP 1 *
FROM Users
WHERE Email = 'student@example.com'
```
EF then maps the result back to a C# User object.