namespace OnlineStore.Repositories;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;

public interface ICityRepository : IGenericRepository<City>
{
    Task<IEnumerable<CityDto>> GetByStateAsync(int stateId);
    Task<IEnumerable<City>> GetAllWithPaginationAsync(string searchTxt ,int pageNumber,int pageSize);
} 