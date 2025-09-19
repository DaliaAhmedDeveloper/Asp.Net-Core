namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.ViewModels;
using OnlineStore.Repositories;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepo;

    public CityService(ICityRepository CityRepo)
    {
        _cityRepo = CityRepo;
    }

    /*=========== API ========================*/

    // List by state id
    public async Task<IEnumerable<CityDto>> ListByState(int stateId)
    {
        return await _cityRepo.GetByStateAsync(stateId);
    }

    /*=========== WEB ========================*/
    // get
    public async Task<IEnumerable<City>> GetAllForWeb()
    {
        return await _cityRepo.GetAllWithTranslationsAsync();
    }
    
    // get all with pagination
     public async Task<PagedResult<City>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _cityRepo.CountAllAsync();
        var cities = await _cityRepo.GetAllWithPaginationAsync(searchTxt, pageNumber, pageSize);
        var model = new PagedResult<City>
        {
            Items = cities,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }
    // get details
    public async Task<City?> GetForWeb(int id)
    {
        return await _cityRepo.GetWithTranslationsAsync(id);
    }

    // add new City
    public async Task<City> CreateForWeb(CityViewModel model)
    {
        var city = new City
        {
            Name = model.Name,
            StateId = model.StateId,
            Translations = new List<CityTranslation>
            {
                new CityTranslation { LanguageCode = "en", Name = model.NameEn },
                new CityTranslation { LanguageCode = "ar", Name = model.NameAr }
            }
        };

        await _cityRepo.AddAsync(city);
        return city;
    }
    // update City
    public async Task<City> UpdateForWeb(CityViewModel model, City city)
    {
        city.Name = model.Name;
        city.StateId = model.StateId;
        foreach (var translation in city.Translations)
        {
            if (translation.LanguageCode == "en")
            {
                translation.Name = model.NameEn;
            }
            else if (translation.LanguageCode == "ar")
            {
                translation.Name = model.NameAr;
            }
        }

        await _cityRepo.UpdateAsync(city);
        return city;
    }
   
    // delete City
    public async Task<bool> DeleteForWeb(int id)
    {
        var City = await _cityRepo.GetByIdAsync(id);
        return await _cityRepo.DeleteAsync(id);
    }
   
} 