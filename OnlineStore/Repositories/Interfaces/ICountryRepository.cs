namespace OnlineStore.Repositories;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;

public interface ICountryRepository : IGenericRepository<Country>
{
    Task<IEnumerable<CountryDto>> GetAllBasedOnLanguage();
    Task<IEnumerable<CountryDto>> GetBasedOnLanguage();
    Task<IEnumerable<Country>> GetAllWithPaginationAsync(string searchTxt, int pageNumber, int pageSize);
} 