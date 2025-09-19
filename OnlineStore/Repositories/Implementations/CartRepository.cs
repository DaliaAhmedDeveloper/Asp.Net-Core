namespace OnlineStore.Repositories;

using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Services;
using System.Threading.Tasks;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _context;
    private readonly ILanguageService _languageService;

    public CartRepository(AppDbContext context, ILanguageService languageService)
    {
        _context = context;
        _languageService = languageService;
    }
    public async Task<Cart?> GetByUserIdAsync(int userId)
    {
        return await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
    }
    public async Task<CartDto?> GetByUserIdDetailsAsync(int userId)
    {
        var language = _languageService.GetCurrentLanguage();
        return await _context.Carts.Where(c => c.UserId == userId).Select(c => new CartDto
        {
            Id = c.Id,
            Items = c.Items.Select(i => new CartItemDto
            {
                Id = i.Id,
                Quantity = i.Quantity,
                ProductVariantId = i.VariantId,
                Product = new ProductSimpleDto
                {
                    Id = i.Product.Id,
                    ImageUrl = i.Product.ImageUrl,
                    Price = i.Product.Price,
                    SalePrice = i.Product.SalePrice,
                    Title = i.Product.Translations.Where(tr => tr.LanguageCode == language)
                    .Select(tr => tr.Name)
                    .FirstOrDefault()
                },
                // i dont want to return this if no cart product vaiant
                ProductVariant = new ProductVariantDto
                {
                    Id = i.ProductVariant.Id,
                    ImageUrl = i.ProductVariant.ImageUrl,
                    VariantAttributes = i.ProductVariant.VariantAttributeValues.Select(vav => new VariantAttributeValueDto
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
                }
            }).ToList()
        }).FirstOrDefaultAsync();
    }

    public async Task<Cart> AddAsync(Cart cart)
    {
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();
        return cart;
    }

    public async Task<bool> UpdateAsync(Cart cart)
    {
        _context.Carts.Update(cart);
       return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int cartId)
    {
        var cart = await _context.Carts.FindAsync(cartId);
        if (cart != null)
        {
            _context.Carts.Remove(cart);
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }
}
