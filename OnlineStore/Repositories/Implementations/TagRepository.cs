using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Services;

namespace OnlineStore.Repositories;

public class TagRepository : GenericRepository<Tag>, ITagRepository
{
    public TagRepository(AppDbContext context, IQueryService query) : base(context)
    {
    }
    // get tag if contain
    public async Task<IEnumerable<Tag>> Contains(List<int> tags)
    {
        return await _context.Tags
                .Where(t => tags.Contains(t.Id))
                .ToListAsync();
    }
    // get all with pagination 
    public  async Task<IEnumerable<Tag>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {
        if (!string.IsNullOrEmpty(searchTxt))
            return await _context.Tags.Include(c => c.Translations).Where(t => t.Code != null && t.Code.Contains(searchTxt) || t.Translations.Any(t => t.Name.Contains(searchTxt))).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await _context.Tags.Include(c => c.Translations).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }
}
