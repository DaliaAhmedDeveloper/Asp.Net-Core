using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Services;

namespace OnlineStore.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    private readonly IQueryService _query;
     private readonly ILanguageService _lang;
    public CategoryRepository(AppDbContext context, ILanguageService lang, IQueryService query) : base(context)
    {
        _query = query;
        _lang = lang;
    }

    // get all based on request language 
    public async Task<IEnumerable<Category>> GetAllBasedOnLangAsync()
    {
        return await _query.BasedOnLanguage(_context.Categories).ToListAsync();
    }
    // return with products
    public async Task<CategoryDetailsDto?> GetWithProductsBasedOnLangAsync(int id)
    {
        var language = _lang.GetCurrentLanguage();
        var category = await _context.Categories.Select(c => new CategoryDetailsDto
        {
            Id = c.Id,
             Slug = c.Slug,
            Name = c.Translations.Where(tr => tr.LanguageCode == language).Select(tr => tr.Name).FirstOrDefault() ?? "",
            Description = c.Translations.Where(tr => tr.LanguageCode == language).Select(tr => tr.Description).FirstOrDefault() ?? "",
            ImageUrl = c.ImageUrl,
            Products = c.Products.Select(p => new ProductSimpleDto
           {
               Id = p.Id,
               Title = p.Translations.Where(tr => tr.LanguageCode == "en").Select(tr => tr.Name).FirstOrDefault() ?? "",
               Price = p.Price,
               SalePrice = p.SalePrice,
               ImageUrl = p.ImageUrl
           }).ToList()
        }).Where(c => c.Id == id).FirstOrDefaultAsync();

        return category;

    }

    // get with translations
    public override async Task<Category?> GetWithTranslationsAsync(int id)
    {
        return await _context.Categories.Include(c => c.Translations).Where(c => c.Id == id).FirstOrDefaultAsync();
    }
    // get all with pagination 
    public  async Task<IEnumerable<Category>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {
        if (!string.IsNullOrEmpty(searchTxt))
            return await _context.Categories.Include(c => c.Translations).Where(c => c.Slug.Contains(searchTxt) || c.Translations.Any(t => t.Name.Contains(searchTxt))).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await _context.Categories.Include(c => c.Translations).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    } 
    // get tag if contain
    public async Task<IEnumerable<Category>> Contains(List<int> categories)
    {
        return await _context.Categories
                .Where(c => categories.Contains(c.Id))
                .ToListAsync();
    }
}
