namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.ViewModels;
using System.Threading.Tasks;
public interface IReturnService
{
    Task<PagedResult<Return>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber = 1, int pageSize = 10);
    Task<Return?> GetForWeb(int id);
    Task<bool> DeleteForWeb(int id);

}
