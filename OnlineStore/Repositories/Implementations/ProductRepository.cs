namespace OnlineStore.Repositories;

using OnlineStore.Models;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Services;
using System.Threading.Tasks;
using OnlineStore.Models.Dtos.Responses;
using FormsDto = OnlineStore.Models.Dtos.Requests;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly ILanguageService _languageService;
    public ProductRepository(AppDbContext context, ILanguageService languageService) : base(context)
    {
        _languageService = languageService;
    }
    // get all with translations
    public async Task<IEnumerable<Product>> GetAllBasedOnLangAsync()
    {
        var language = _languageService.GetCurrentLanguage();
        return await _context.Products.Include(t => t.Translations.Where(tr => tr.LanguageCode == language)).ToListAsync();
    }
    // get all with relations 
    public async Task<IEnumerable<ProductV2Dto>> GetAllWithRelationsBasedOnLangAsync()
    {
        // here needs also categories tags translations also
        // need to fetch attributes
        var language = _languageService.GetCurrentLanguage();

        return await _context.Products
            .Select(p => new ProductV2Dto
            {
                Id = p.Id,
                Price = p.Price,
                SalePrice = p.SalePrice,
                Translations = p.Translations
                    .Where(tr => tr.LanguageCode == language)
                    .ToList(),

                Tags = p.Tags
                    .Where(t => t.Translations.Any(tr => tr.LanguageCode == language))
                    .Select(t => new TagV2Dto
                    {
                        Id = t.Id,
                        Translations = t.Translations
                            .Where(tr => tr.LanguageCode == language)
                            .ToList()
                    }).ToList(),

                Categories = p.Categories
                    .Where(c => c.Translations.Any(tr => tr.LanguageCode == language))
                    .Select(c => new CategoryV2Dto
                    {
                        Id = c.Id,
                        Translations = c.Translations
                            .Where(tr => tr.LanguageCode == language)
                            .ToList()
                    }).ToList(),

                ProductVariants = p.ProductVariants.ToList(),
                Reviews = p.Reviews.ToList()
            })
            .ToListAsync();
    }
    // get Product 
    public override async Task<Product?> GetByIdAsync(int productId)
    {
        return await _context.Products.Include(p => p.ProductVariants).FirstOrDefaultAsync(p => p.Id == productId);
    }
    // get Product details
    public async Task<ProductDto?> GetByIdWithAllRelationsAsync(int productId)
    {
        var language = _languageService.GetCurrentLanguage();
        return await _context.Products
        .Where(p => p.Id == productId)
        .Select(p => new ProductDto
        {
            Id = p.Id,
            Price = p.Price,
            SalePrice = p.SalePrice,
            Title = p.Translations
                    .Where(tr => tr.LanguageCode == language)
                    .Select(tr => tr.Name)
                    .FirstOrDefault(),
            Description = p.Translations
                    .Where(tr => tr.LanguageCode == language)
                    .Select(tr => tr.Description)
                    .FirstOrDefault(),
            Brand = p.Translations
                    .Where(tr => tr.LanguageCode == language)
                    .Select(tr => tr.Brand)
                    .FirstOrDefault(),
            ImageUrl = p.ImageUrl,
            Categories = p.Categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Slug = c.Slug,
                Title = c.Translations
                    .Where(tr => tr.LanguageCode == language)
                    .Select(tr => tr.Name)
                    .FirstOrDefault(),
                Description = c.Translations
                    .Where(tr => tr.LanguageCode == language)
                    .Select(tr => tr.Description)
                    .FirstOrDefault(),
            }).ToList(),
            Tags = p.Tags.Select(t => new TagDto
            {
                Id = t.Id,
                Code = t.Code,
                Title = t.Translations
                    .Where(tr => tr.LanguageCode == language)
                    .Select(tr => tr.Name)
                    .FirstOrDefault(),
            }).ToList(),
            ProductVariants = p.ProductVariants.Select(pv => new ProductVariantDto
            {
                Id = pv.Id,
                ImageUrl = pv.ImageUrl,
                VariantAttributes = pv.VariantAttributeValues.Select(vav => new VariantAttributeValueDto
                {
                    // attribute name 
                    Attribute = new AttributeDto
                    {
                        Id = vav.Attribute.Id,
                        Slug = vav.Attribute.Code,
                        Title = vav.Attribute.Translations
                        .Where(tr => tr.LanguageCode == language)
                        .Select(tr => tr.Name)
                        .FirstOrDefault(),
                        AttributeValue = new AttributeValueDto
                        {
                            Id = vav.AttributeValue.Id,
                            Slug = vav.AttributeValue.Code,
                            Title = vav.AttributeValue.Translations
                                .Where(tr => tr.LanguageCode == language)
                                .Select(tr => tr.Name)
                                .FirstOrDefault(),
                        },
                    },

                }).ToList()
            }).ToList()
        })
        .FirstOrDefaultAsync();
    }
    // update Product

    // product search by text or SKU
    public async Task<IEnumerable<Product>> GetSearchProductsAsync(string searchText)
    {
        var language = _languageService.GetCurrentLanguage();
        return await _context.Products
                    .Include(t => t.Translations.Where(tr => tr.LanguageCode == language))
                    .Where(p => p.SKU.Contains(searchText) || p.Translations.Any(tr => tr.Name.ToLower().Contains(searchText.ToLower())))
                    .ToListAsync();
    }

    // filter by category , filter by tags , filter by attribute value , filter by price , filter by text
    public async Task<IEnumerable<Product>> FilterProductsAsync(FormsDto.ProductFilterDto filterDto)
    {
        var language = _languageService.GetCurrentLanguage();

        var query = _context.Products
            .Include(p => p.Translations.Where(tr => tr.LanguageCode == language))
            .AsQueryable();

        // Optional Price filter
        if (filterDto.PriceFrom.HasValue)
            query = query.Where(p => p.Price >= filterDto.PriceFrom.Value);

        if (filterDto.PriceTo.HasValue)
            query = query.Where(p => p.Price <= filterDto.PriceTo.Value);

        // Optional Tag filter
        if (filterDto.TagId > 0)
            query = query.Where(p => p.Tags.Any(t => t.Id == filterDto.TagId));

        // Optional Category filter
        if (filterDto.CategoryId > 0)
            query = query.Where(p => p.Categories.Any(c => c.Id == filterDto.CategoryId));

        // Optional AttributeValue filter
        if (filterDto.AttributeValueId > 0)
            query = query.Where(p => p.ProductVariants
                                     .Any(v => v.VariantAttributeValues
                                                .Any(vav => vav.Id == filterDto.AttributeValueId)));

        return await query.ToListAsync();
    }
    // get all with pagination 
    public async Task<IEnumerable<Product>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10,
        bool? tracking = false
        )
    {
        IQueryable<Product> query = _context.Products;
        if (tracking == true)
        {
            query = query.AsTracking();
        }
        if (!string.IsNullOrEmpty(searchTxt))
            return await
            query
            .Include(p => p.Translations)
            .Include(p => p.Categories)
            .ThenInclude(pc => pc.Translations)
            .Where(p => p.Slug.Contains(searchTxt) || p.Translations.Any(t => t.Name.Contains(searchTxt))).Skip((pageNumber - 1) * pageSize).Take(pageSize)
            .ToListAsync();

        return await query
        .Include(c => c.Translations)
        .Include(p => p.Categories)
        .ThenInclude(pc => pc.Translations)
        .Skip((pageNumber - 1) * pageSize).Take(pageSize)
        .ToListAsync();
    }
    // unique sku
    public async Task<bool> CheckSkuAsync(string sku)
    {
        return await _context.Products.AnyAsync(p => p.SKU == sku);
    }
    // get with relations no tracking for web
    public async Task<Product?> GetWithAllRelationsAsync(int id, bool? tracking = false)
    {
        IQueryable<Product> query = _context.Products;
        if (tracking == true)
        {
            query = query.AsTracking(); // to stop ef tracking and return lot of un required relations
        }
        return await query.Where(p => p.Id == id)
        .Include(p => p.Categories)
        .Include(p => p.Translations)
        .Include(p => p.Tags)
        .Include(p => p.ProductVariants.Where(pv => pv.IsDefault == true))
        .ThenInclude(pv => pv.Stock)
        .Include(p => p.ProductVariants.Where(pv => pv.IsDefault == true))
        .ThenInclude(pv => pv.VariantAttributeValues)
        .FirstOrDefaultAsync();
    }
    // latest 
    
    public override async Task<IEnumerable<Product>> GetLatestAsync()
    {
        return await _dbSet.OrderByDescending(p => p.CreatedAt)
        .Include(p => p.ProductVariants)
        .ThenInclude(pv => pv.Stock)
        .Take(4).ToListAsync();
    }

}