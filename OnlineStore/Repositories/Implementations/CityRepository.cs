namespace OnlineStore.Repositories;

using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Services;

public class CityRepository : GenericRepository<City>, ICityRepository
{
    private readonly ILanguageService _language;
    public CityRepository(AppDbContext context, ILanguageService language) : base(context)

    {
        _language = language; 
    }

    // get all cities based on language by state id
    public async Task<IEnumerable<CityDto>> GetByStateAsync(int stateId)
    {
        string lang = _language.GetCurrentLanguage();
        return await _context.Cities.Where(c => c.StateId == stateId).Select(c => new CityDto
        {
            Id = c.Id,
            Name = c.Translations.Where(tr => tr.LanguageCode == lang).Select(tr => tr.Name).FirstOrDefault() ?? ""
        }).ToListAsync();
    }
     // get all with pagination 
    public  async Task<IEnumerable<City>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {
        if (!string.IsNullOrEmpty(searchTxt))
            return await _context.Cities.Include(c => c.Translations).Where(c => c.Name.Contains(searchTxt) || c.Translations.Any(ct => ct.Name.Contains(searchTxt))).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await _context.Cities.Include(c => c.Translations).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }
} 