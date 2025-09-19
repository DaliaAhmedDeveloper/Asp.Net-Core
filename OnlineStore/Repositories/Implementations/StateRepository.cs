namespace OnlineStore.Repositories;

using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Services;

public class StateRepository : GenericRepository<State>, IStateRepository
{
    private readonly ILanguageService _language;
    public StateRepository(AppDbContext context, ILanguageService language) : base(context)

    {
        _language = language;
    }
    // get all states based on language by country id
    public async Task<IEnumerable<StateDto>> GetByCountryAsync(int countryId)
    {
        string lang = _language.GetCurrentLanguage();
        return await _context.States.Select(c => new StateDto
        {
            Id = c.Id,
            Name = c.Translations.Where(tr => tr.LanguageCode == lang).Select(tr => tr.Name).FirstOrDefault() ?? ""
        }).ToListAsync();
    }

    // get all with pagination 
    public async Task<IEnumerable<State>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {
        if (!string.IsNullOrEmpty(searchTxt))
            return await _context.States.Include(s => s.Translations).Where(s => s.Code.Contains(searchTxt) || s.Translations.Any(ct => ct.Name.Contains(searchTxt))).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await _context.States.Include(s => s.Translations).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }
} 


  