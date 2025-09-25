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

# install EF packages :
dotnet add package Microsoft.EntityFrameworkCore -> core EF Package
dotnet add package Microsoft.EntityFrameworkCore.SqlServer -> SQL Server Provider
dotnet add package Microsoft.EntityFrameworkCore.Tools  -> for migrations

# Entity Framework Core (EF Core) – What It Provides

EF Core is a modern ORM (Object-Relational Mapper) for .NET, and it gives you a lot of functionality to work with databases without writing raw SQL.

## Core Components
DbContext                     -> Main class for interacting with the database. Tracks changes, saves data, queries tables.
DbSet<T>                      -> Represents a table of entities (Users, Courses, etc.). Allows LINQ queries and CRUD operations.
Entity classes                -> C# classes that represent your database tables.
Fluent API / Data Annotations -> Configure relationships, keys, constraints, table names, column types.
Migrations                    -> Create/update database schema from your code (Add-Migration, Update-Database).

## LINQ Methods Supported by EF Core

EF Core translates LINQ queries into SQL queries automatically.

Common Methods:
ToListAsync()	                Get all results as a list asynchronously.
FirstOrDefaultAsync(predicate)	Get the first matching record or null.
SingleOrDefaultAsync(predicate)	Get single matching record or null (throws if more than one).
Where(predicate)	            Filter results based on condition.
AnyAsync(predicate)          	Check if any records exist.
CountAsync(predicate)	        Count records.
Include(navigation)	            Load related entities (eager loading).
OrderBy() / OrderByDescending()	Sort results.
Skip() / Take()	                Pagination.
Select()	                    Project only specific columns.

## Change Tracking
EF Core tracks changes to your entities automatically:

Added → EntityState.Added
Modified → EntityState.Modified
Deleted → EntityState.Deleted

You can access this via:
dbContext.ChangeTracker.Entries()

## Transactions
EF Core provides built-in support for transactions:

```csharp
using var transaction = await dbContext.Database.BeginTransactionAsync();
try
{
    // multiple operations
    await dbContext.SaveChangesAsync();
    await transaction.CommitAsync();
}
catch
{
    await transaction.RollbackAsync();
}
```
## Raw SQL Queries

EF Core allows executing custom SQL:
```csharp
var courses = await dbContext.Courses
    .FromSqlRaw("SELECT * FROM Courses WHERE Price > 100")
    .ToListAsync();
```
## Database Operations / Helpers
```csharp
dbContext.Database.EnsureCreated() // Create DB if not exists
dbContext.Database.Migrate() // Apply migrations
dbContext.Database.CanConnect() // Check connection
dbContext.SaveChanges() / SaveChangesAsync() // Save changes to DB
```

## Relationships & Navigation
One-to-many → Teacher → Courses
Many-to-many → Students ↔ Courses
One-to-one → Student ↔ Profile

Configured with Fluent API or Data Annotations.

## Global Configuration
You can configure default schema, table naming, logging, lazy loading in OnConfiguring or in Program.cs.

9️⃣ Advanced Features

- Shadow properties → properties in EF model but not in C# class
- Computed columns → DB-generated values
- Concurrency tokens → handle conflicting updates
  Example use case: stock
  Two users try to buy 5 items at the same time.
  Both read Stock = 7.
  Both subtract 5 → Stock = 2 another subtract 5 → Stock = -3.
  EF detects that the RowVersion changed for the second user → throws DbUpdateConcurrencyException before subtract another 5.
  You can then reload the stock and show the updated value.

- Value conversions → map C# types to DB types
- Query filters → global filters for soft deletes, multi-tenancy

