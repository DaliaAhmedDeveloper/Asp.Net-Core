namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;

public interface ICartItemService
{
    Task<CartItem> Add(CartItemDto cartItemDto);
    Task<Cart?> Update(int userId, UpdateCartDto updateCartDto);
    Task<bool> Delete(int cartItemId);
    Task<Cart> AddToCart(int userId, AddToCartDto addToCartDto);
    Task<IEnumerable<CartItem>> ListByCart(int cartId);
}