namespace OnlineStore.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.ViewModels;

public interface ICategoryService
{
    // api
    Task<CategoryDetailsDto> Find(int id);
    Task<IEnumerable<Category>> List();

    // web
    Task<Category> CreateForWeb(CategoryViewModel model);
    Task<Category?> GetForWeb(int id);
    Task<IEnumerable<Category>> GetAllForWeb();
    Task<PagedResult<Category>> GetAllWithPaginationForWeb(string searchTxt , int pageNumber, int pageSize);
    Task<Category> UpdateForWeb(CategoryViewModel model, Category category);
    Task<bool> DeleteForWeb(int id);
}
