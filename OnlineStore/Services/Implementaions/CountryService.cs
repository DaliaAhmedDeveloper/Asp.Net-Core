namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.ViewModels;
using OnlineStore.Repositories;

public class CountryService : ICountryService 
{
    private readonly ICountryRepository _countryRepo;

    public CountryService(ICountryRepository countryRepo)
    {
        _countryRepo = countryRepo;
    }

    /*=========== API ========================*/

    // get all active countries with details (relations , cities , ststes) based oon current language
    public async Task<IEnumerable<CountryDto>> ListAllWithDetailsBasedOnLaguage()
    {
        return await _countryRepo.GetAllBasedOnLanguage();
    }
    // get all without relations based on current language
    public async Task<IEnumerable<CountryDto>> ListBasedOnLaguage()
    {
        return await _countryRepo.GetBasedOnLanguage();
    }

    /*=========== WEB ========================*/
     // get
    public async Task<IEnumerable<Country>> GetAllForWeb()
    {
        return await _countryRepo.GetAllWithTranslationsAsync();
    }
    // get all with pagination
     public async Task<PagedResult<Country>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _countryRepo.CountAllAsync();
        var cities = await _countryRepo.GetAllWithPaginationAsync(searchTxt, pageNumber, pageSize);
        var model = new PagedResult<Country>
        {
            Items = cities,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }
    // get details
    public async Task<Country?> GetForWeb(int id)
    {
        return await _countryRepo.GetWithTranslationsAsync(id);
    }

    // add new Country
    public async Task<Country> CreateForWeb(CountryViewModel model)
    {
        var Country = new Country
        {
            Code = model.Code,
            Translations = new List<CountryTranslation>
            {
                new CountryTranslation { LanguageCode = "en", Name = model.NameEn },
                new CountryTranslation { LanguageCode = "ar", Name = model.NameAr }
            }
        };

        await _countryRepo.AddAsync(Country);
        return Country;
    }
    // update Country
    public async Task<Country> UpdateForWeb(CountryViewModel model, Country Country)
    {
        Country.Code = model.Code;
        foreach (var translation in Country.Translations)
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

        await _countryRepo.UpdateAsync(Country);
        return Country;
    }
   
    // delete Country
    public async Task<bool> DeleteForWeb(int id)
    {
        var Country = await _countryRepo.GetByIdAsync(id);
        return await _countryRepo.DeleteAsync(id);
    }
   
    public async Task<bool> ActivateCountryAsync(int id)
    {
        var country = await _countryRepo.GetByIdAsync(id);
        if (country == null)
            return false;

        country.IsActive = true;
        country.UpdatedAt = DateTime.UtcNow;
        await _countryRepo.UpdateAsync(country);
        return true;
    }

    public async Task<bool> DeactivateCountryAsync(int id)
    {
        var country = await _countryRepo.GetByIdAsync(id);
        if (country == null)
            return false;

        country.IsActive = false;
        country.UpdatedAt = DateTime.UtcNow;
        await _countryRepo.UpdateAsync(country);
        return true;
    }
} 