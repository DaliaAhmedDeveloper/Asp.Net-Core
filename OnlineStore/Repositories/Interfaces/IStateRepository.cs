namespace OnlineStore.Repositories;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;

public interface IStateRepository : IGenericRepository<State>
{
    Task<IEnumerable<StateDto>> GetByCountryAsync(int countryId);
    Task<IEnumerable<State>> GetAllWithPaginationAsync(string searchTxt, int pageNumber, int pageSize);
} 