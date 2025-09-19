namespace OnlineStore.Services;

using Microsoft.Extensions.Localization;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Repositories;
using System.Threading.Tasks;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepo;
    private readonly IStringLocalizer<CartService> _localizer;

    public CartService(ICartRepository cartRepo, IStringLocalizer<CartService> localizer)
    {
        _cartRepo = cartRepo;
        _localizer = localizer;
    }
    public async Task<Cart?> FindByUserId(int userId)
    {
        return await _cartRepo.GetByUserIdAsync(userId);
    }
    public async Task<CartDto?> FindDetailsByUserId(int userId)
    {
        return await _cartRepo.GetByUserIdDetailsAsync(userId);
    }
    public async Task<Cart> Add(Cart cart)
    {
        return await _cartRepo.AddAsync(cart);
    }

    public async Task<bool> Update(Cart cart)
    {
        return await _cartRepo.UpdateAsync(cart);
    }

    public async Task<bool> Remove(int cartId)
    {
        return await _cartRepo.DeleteAsync(cartId);
    }

    /*
 Check User Cart
 Check if Cart is empty
 Check if order items found inside cart 
 */
    public async Task<CartDto> CheckUserCart(int userId)
    {
        // get user cart with items ,, check for not found
        var userCart = await _cartRepo.GetByUserIdDetailsAsync(userId);
        if (userCart == null)
            throw new NotFoundException(_localizer["CartNotFound"]);

        if (userCart.Items == null || !userCart.Items.Any())
            throw new NotFoundException(_localizer["CartEmpty"]);

        return userCart;
    }
}
