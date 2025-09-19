namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.ViewModels;
using OnlineStore.Repositories;

public class StateService : IStateService
{
    private readonly IStateRepository _stateRepo;

    public StateService(IStateRepository stateRepo)
    {
        _stateRepo = stateRepo;
    }

    /*=========== API ========================*/

    // List by country id
    public async Task<IEnumerable<StateDto>> ListByCountry(int countryId)
    {
        return await _stateRepo.GetByCountryAsync(countryId);
    }

    /*=========== WEB ========================*/

    // get all
    public async Task<IEnumerable<State>> GetAllForWeb()
    {
        return await _stateRepo.GetAllWithTranslationsAsync();
    }

    // get all with pagination
    public async Task<PagedResult<State>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _stateRepo.CountAllAsync();
        var cities = await _stateRepo.GetAllWithPaginationAsync(searchTxt, pageNumber, pageSize);
        var model = new PagedResult<State>
        {
            Items = cities,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }
    // get by id
    public async Task<State?> GetForWeb(int id)
    {
        return await _stateRepo.GetWithTranslationsAsync(id);
    }
    // create for web
    public async Task<State> CreateForWeb(StateViewModel model)
    {
        var state = new State
        {
            Code = model.Code,
            CountryId = model.CountryId,
            Translations = new List<StateTranslation>
            {
                new StateTranslation { LanguageCode = "en", Name = model.NameEn },
                new StateTranslation { LanguageCode = "ar", Name = model.NameAr }
            }
        };
        await _stateRepo.AddAsync(state);
        return state;
    }
    // update for web
    public async Task<State> UpdateForWeb(StateViewModel model, State state)
    {
        state.Code = model.Code;
        state.CountryId = model.CountryId;
        foreach (var translation in state.Translations)
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

        await _stateRepo.UpdateAsync(state);
        return state;
    }

    // delete for web
    public async Task<bool> DeleteForWeb(int id)
    {
        var State = await _stateRepo.GetByIdAsync(id);
        if (State == null)
            return false;

        State.IsDeleted = true;
        State.UpdatedAt = DateTime.UtcNow;
        await _stateRepo.UpdateAsync(State);
        return true;
    }
}