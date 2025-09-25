namespace OnlineStore.Repositories;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<IEnumerable<Category>> GetAllBasedOnLangAsync();
    Task<CategoryDetailsDto?> GetWithProductsBasedOnLangAsync(int id);
    Task<IEnumerable<Category>> GetAllWithPaginationAsync(string searchTxt, int page, int pageSize);
    Task<IEnumerable<Category>> Contains(List<int> categories);
    Task<Category?> GetBySlug(string slug);
    Task<Category?> GetByIdAndRelationsAsync(int id);
    Task<bool> DeleteAsync(Category category);
}