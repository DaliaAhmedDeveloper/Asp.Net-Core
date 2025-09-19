namespace OnlineStore.Services;
using OnlineStore.Models.ViewModels;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;

public interface ICityService
{
    // api
    Task<IEnumerable<CityDto>> ListByState(int stateId);

    // web
    Task<City> CreateForWeb(CityViewModel model);
    Task<City?> GetForWeb(int id);
    Task<PagedResult<City>> GetAllWithPaginationForWeb(string searchTxt , int pageNumber, int pageSize);
    Task<IEnumerable<City>> GetAllForWeb();
    Task<City> UpdateForWeb(CityViewModel model, City city);
    Task<bool> DeleteForWeb(int id);
} 