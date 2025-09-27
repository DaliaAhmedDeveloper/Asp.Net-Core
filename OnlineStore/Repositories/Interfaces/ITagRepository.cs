namespace OnlineStore.Repositories;
using OnlineStore.Models;
public interface ITagRepository : IGenericRepository<Tag>
{
    Task<IEnumerable<Tag>> Contains(List<int> tags);
    Task<IEnumerable<Tag>> GetAllWithPaginationAsync(string searchTxt, int page, int pageSize);
    Task<Tag?> GetByCode(string code);
    Task<Tag?> GetByIdAndRelationsAsync(int id);
}