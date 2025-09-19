namespace OnlineStore.Services;

public interface IQueryService
{
    IQueryable<T> BasedOnLanguage<T>(IQueryable<T> queryable) where T : class;
}
