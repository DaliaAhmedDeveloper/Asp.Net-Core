using Microsoft.EntityFrameworkCore;
using OnlineStore.Helpers;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Services;

namespace OnlineStore.Repositories;

public class WishlistRepository : IWishlistRepository
{
    private readonly AppDbContext _context;
    private readonly ILanguageService _languageService;
    public WishlistRepository(AppDbContext context, ILanguageService languageService)
    {
        _context = context;
        _languageService = languageService;
    }
    // get all by user-
    public async Task<IEnumerable<WishlistDto>> GetAllByUserAsync(int userId)
    {
        var language = _languageService.GetCurrentLanguage();
        return await _context.Wishlist.Where(w => w.UserId == userId).Select(w => new WishlistDto
        {
            Id = w.Id,
            product = new ProductSimpleDto
            {
                Id = w.Product.Id,
                Price = w.Product.Price,
                SalePrice = w.Product.SalePrice,
                ImageUrl = w.Product.ImageUrl,
                Title = w.Product.Translations.Where(tr => tr.LanguageCode == language).Select(tr=> tr.Name).FirstOrDefault() ??""
            }
        }).ToListAsync();
    }
    // get by userid and product id
    public async Task<Wishlist?> GetByUserAndProduct(int userId, int productId)
    {
        return await _context.Wishlist.FirstOrDefaultAsync(w => w.ProductId == productId && w.UserId == userId);
    }
    // add new Wishlist 
    public async Task<Wishlist> AddAsync(Wishlist wishlist)
    {
        _context.Wishlist.Add(wishlist);
        await _context.SaveChangesAsync();
        return wishlist;
    }
    // remove Wishlist
    public async Task<bool> DeleteAsync(int wishlistId)
    {
        var wishlist = await _context.Wishlist.FindAsync(wishlistId);
        if (wishlist == null)
            return false;

        _context.Wishlist.Remove(wishlist);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}