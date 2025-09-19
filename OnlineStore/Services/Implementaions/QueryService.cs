namespace OnlineStore.Services;
using Microsoft.EntityFrameworkCore;
public class QueryService : IQueryService
{
    protected readonly ILanguageService _languageService;
    public QueryService(ILanguageService languageService)
    {
        _languageService = languageService;
    }
    public IQueryable<T> BasedOnLanguage<T>(IQueryable<T> queryable)
        where T : class
    {
        return queryable.Include(entity =>
            EF.Property<IEnumerable<object>>(entity, "Translations")
                .Where(t => EF.Property<string>(t, "LanguageCode") == _languageService.GetCurrentLanguage())
        );
    }
    
}