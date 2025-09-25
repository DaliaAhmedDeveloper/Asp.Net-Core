## DbContextOptions<TContext>

This object contains all the settings needed to configure the DbContext.

It is used in the constructor to specify:

- Database type (SQL Server, SQLite, PostgreSQL, etc.)
- Connection string
- Logging, Tracking behavior
- Any additional EF Core settings

### Example  ( database connection ) 
```csharp 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// options for database types
options.UseSqlServer("connectionString");
options.UseSqlite("connectionString");
options.UseNpgsql("connectionString"); // PostgreSQL
options.UseMySql("connectionString"); 
```
DefaultConnection inside appSettings.json file ,, it automatically looks for database connection strings inside appSettings.json file 

```json 
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=store;Trusted_Connection=True;TrustServerCertificate=True;"
  }
  ```

### Example ( Query Tracking ) 
```csharp 
options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
```

### Example ( Logging / Debugging ) 
```csharp 
options.LogTo(Console.WriteLine); // Prints queries to the console
options.EnableSensitiveDataLogging(); // Adds actual data to the log
options.EnableDetailedErrors(); // Displays detailed errors
```
### Example ( Lazy Loading / Proxies ) 
```csharp 
options.UseLazyLoadingProxies();
```
Enables Lazy Loading: When you retrieve an object linked to the main object, EF automatically retrieves the data upon request.

### Example ( Command Timeout) 
```csharp 
options.UseSqlServer(connectionString, sql => sql.CommandTimeout(180));
```
Specifies the maximum execution time before EF throws an Exception.

### Example ( Enable/Disable Sensitive Logging ) 
```csharp 
options.EnableSensitiveDataLogging(); // Displays actual values ​​in the logs
```

### Example ( Batch Behavior ) 
```csharp 
options.MaxBatchSize(100); // Maximum number of commands in a single batch
```
### Full Example 
```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    options.LogTo(Console.WriteLine, LogLevel.Information);
    options.EnableDetailedErrors();
});
```