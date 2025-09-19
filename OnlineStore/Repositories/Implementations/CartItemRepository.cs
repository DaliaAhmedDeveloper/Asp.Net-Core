using Microsoft.EntityFrameworkCore;
using OnlineStore.Helpers;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;

namespace OnlineStore.Repositories;

public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
{
    public CartItemRepository(AppDbContext context) : base(context)
    {
    }

    // get all by cart
    public async Task<IEnumerable<CartItem>> GetAllByCartAsync(int cartId)
    {
        return await _context.CartItems.Where(a => a.CartId == cartId).ToListAsync();
    }

    // get by productId and Variant Id
    public async Task<CartItem?> GetByProductIdAndVariantIdAsync(int cartId, int productId, int? variantId)
    {
        return await _context.CartItems.FirstOrDefaultAsync(ci =>
         ci.CartId == cartId &&
         ci.ProductId == productId &&
         ci.VariantId == variantId);
    }
    // Delete All CartItem
    public async Task<bool> DeleteAllAsync(int userId)
    {
        var userCartItems = await _context.CartItems
            .Where(ci => ci.Cart.UserId == userId)
            .ToListAsync();

        if (!userCartItems.Any())
            return false; 

        _context.CartItems.RemoveRange(userCartItems);
        return await _context.SaveChangesAsync() > 0;

    }
}