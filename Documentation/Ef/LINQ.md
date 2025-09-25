
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
