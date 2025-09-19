# What is pagination ?

Pagination is the process of dividing large sets of data into smaller, more manageable chunks (pages) instead of displaying everything at once.

## in ASP.NET Core MVC or Web API is usually done in two parts:

- Querying only the needed data from the database.

```csharp
int pageNumber = page ?? 1; // current page , page comes from the query string (URL).
int pageSize = 10;          // items per page

categories = await _db.Categories
    .OrderBy(c => c.Name)
    .Skip((pageNumber - 1) * pageSize) // skip previous pages
    .Take(pageSize) // take only the number of items for this page
    .ToListAsync();

```

- Sending the pagination info to the view or API response.

1. Create View Model
```csharp
public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; } = new List<T>();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
}
```
2. Inside Controller
```csharp
 var model = new PagedResult<Category>
    {
        Items = categories,
        PageNumber = pageNumber,
        PageSize = pageSize,
        TotalItems = totalItems
    };

    return View(model);
```

3. inside Razor view :
```html

<nav>
    <ul class="pagination">
        @for (int i = 1; i <= Model.TotalPages; i++)
        {
            <li class="page-item @(i == Model.PageNumber ? "active" : "")">
                <a class="page-link" asp-action="Index" asp-route-page="@i">@i</a>
            </li>
        }
    </ul>
</nav>

```
