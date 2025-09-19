namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.ViewModels;

public interface ICountryService
{
    //api
    Task<IEnumerable<CountryDto>> ListAllWithDetailsBasedOnLaguage();
    Task<IEnumerable<CountryDto>> ListBasedOnLaguage();

     // web
    Task<Country> CreateForWeb(CountryViewModel model);
    Task<PagedResult<Country>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber, int pageSize);
    Task<Country?> GetForWeb(int id);
    Task<IEnumerable<Country>> GetAllForWeb();
    Task<Country> UpdateForWeb(CountryViewModel model, Country country);
    Task<bool> DeleteForWeb(int id);
    Task<bool> ActivateCountryAsync(int id);
    Task<bool> DeactivateCountryAsync(int id);
} 