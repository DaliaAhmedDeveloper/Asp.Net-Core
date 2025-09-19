# Steps For Database Translation

# First Step :
Ceate separate table for each table needs translation
this is the best practice for scalability when you need to add more languages 
so name_en name_ar inside same table not the best practice 

Products Table : Id Price Stock etc
ProductTranslations Table : Id Product_id LangCode Name Description

if u have multiple languages will be like : 
 1 1 en dress good dress
 2 1 ar ...   ........
 3 1 fr ...   ........

# second Step :
Create a model for both tables 
Index the ProductId and LanguageCode inside the ProductTranslations table to improve performance:

```sql
CREATE INDEX IX_ProductTranslations_ProductId_LanguageCode
ON ProductTranslations(ProductId, LanguageCode);
```

```csharp
public class Product
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public ICollection<ProductTranslation> Translations { get; set; }
}

public class ProductTranslation
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public Product Product { get; set; } = null!;
}
```
# third Step : 
Fetch from database based on http request accept/lang value

- create service called Language Service to retuen the http request language value 

```csharp

namespace OnlineStore.Services.Implementaions;
using OnlineStore.Helpers;
using OnlineStore.Services.Interfaces;

public class LanguageService : ILanguageService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LocalizationHelper _localizationHelper;

    public LanguageService(IHttpContextAccessor httpContextAccessor, LocalizationHelper localizationHelper)
    {
        _httpContextAccessor = httpContextAccessor;
        _localizationHelper = localizationHelper;
    }

    public string GetCurrentLanguage()
    {
        return _localizationHelper.GetPreferredLanguage(_httpContextAccessor);
    }
}

```

- inside the model repository : 
 ```csharp
   private readonly ILanguageService _languageService;

    public ProductRepository(AppDbContext context, ILanguageService languageService)
    {
        _context = context;
        _languageService = languageService;
    }
    public IQueryable<Product> QueryBased()
    {
        string lang = _languageService.GetCurrentLanguage();
        return _context.Products
            .Include(p => p.Translations.Where(t => t.LanguageCode == lang));
    }
    public IEnumerable<Product> GetAll()
    {
        return QueryBased().ToList();
    }

 ```
