# Issue :
warn: Microsoft.EntityFrameworkCore.Model.Validation[10622]
      Entity 'User' has a global query filter defined and is the required end of a relationship with the entity 'Address'. This may lead to unexpected results when the required entity is filtered out. Either configure the navigation as optional, or define matching query filters for both entities in the navigation. See https://go.microsoft.com/fwlink/?linkid=2131316 for more information.

Means you apply soft delete to User but there is a relation between it and address

# Cause Of Issue : 

```csharp
 foreach (var entityType in modelBuilder.Model.GetEntityTypes())
 {
    if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
    {
        modelBuilder.Entity(entityType.ClrType)
        .HasQueryFilter(ConvertFilterExpression(entityType.ClrType)); // this line HasQueryFilter method 
        }
    }
```

# Why EF show this warning ?
A global query filter like:
modelBuilder.Entity<User>()
    .HasQueryFilter(u => !u.IsDeleted);
does not only hide the User from direct queries — it also affects any navigation to that User from related entities.

# solution : 
Make the relationship required false 

```csharp
 modelBuilder.Entity<Address>()
    .HasOne(a => a.User)
    .WithMany()
    ;
```

This Tell EF Core how to handle it when the related entity is missing or filtered out.



