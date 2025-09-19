namespace OnlineStore.Services;
using OnlineStore.Models.ViewModels;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;

public interface IStateService
{
    // api
    Task<IEnumerable<StateDto>> ListByCountry(int countryId);

     // web
    Task<State> CreateForWeb(StateViewModel model);
    Task<State?> GetForWeb(int id);
    Task<PagedResult<State>> GetAllWithPaginationForWeb(string searchTxt , int pageNumber, int pageSize);
    Task<IEnumerable<State>> GetAllForWeb();
    Task<State> UpdateForWeb(StateViewModel model, State state);
    Task<bool> DeleteForWeb(int id);
} 