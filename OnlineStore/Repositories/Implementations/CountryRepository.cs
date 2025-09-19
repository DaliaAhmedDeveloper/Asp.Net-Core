namespace OnlineStore.Repositories;

using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Services;

public class CountryRepository : GenericRepository<Country>, ICountryRepository
{
    private readonly ILanguageService _language;
    public CountryRepository(AppDbContext context, ILanguageService language) : base(context)

    {
        _language = language;
    }

    // get all with relations based on current language
    public async Task<IEnumerable<CountryDto>> GetAllBasedOnLanguage()
    {
        string lang = _language.GetCurrentLanguage();
        // return await _query.BasedOnLanguage(_context.Countries)
        //     .Where(c => c.IsActive)
        //     .Include(c => c.States)
        //     .ThenInclude(city => city.Translations.Where(t => t.LanguageCode == _language.GetCurrentLanguage()))
        //     .ToListAsync();
        return await _context.Countries.Where(c => c.IsActive).Select(c => new CountryDto
        {
            Id = c.Id,
            Code = c.Code,
            PhoneCode = c.PhoneCode,
            Name = c.Translations.Where(tr => tr.LanguageCode == lang).Select(tr => tr.Name).FirstOrDefault() ?? "",
            States = c.States.Select(s => new StateDto
            {

                Id = s.Id,
                Name = s.Translations.Where(tr => tr.LanguageCode == lang).Select(tr => tr.Name).FirstOrDefault() ?? "",
                Cities = s.Cities.Select(ci => new CityDto
                {
                    Id = ci.Id,
                    Name = ci.Translations.Where(tr => tr.LanguageCode == lang).Select(tr => tr.Name).FirstOrDefault() ?? "",
                }).ToList()
            }).ToList()

        }).ToListAsync();
    }

    // get all without relations based on current language
    public async Task<IEnumerable<CountryDto>> GetBasedOnLanguage()
    {
        string lang = _language.GetCurrentLanguage();
        return await _context.Countries.Where(c => c.IsActive).Select(c => new CountryDto
        {
            Id = c.Id,
            Code = c.Code,
            PhoneCode = c.PhoneCode,
            Name = c.Translations.Where(tr => tr.LanguageCode == lang).Select(tr => tr.Name).FirstOrDefault() ?? ""
        }).ToListAsync();
    }
    // get all with pagination 
    public async Task<IEnumerable<Country>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {
        if (!string.IsNullOrEmpty(searchTxt))
            return await _context.Countries.Include(c => c.Translations).Where(c => c.Code.Contains(searchTxt) || c.Translations.Any(ct => ct.Name.Contains(searchTxt))).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await _context.Countries.Include(c => c.Translations).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }
}