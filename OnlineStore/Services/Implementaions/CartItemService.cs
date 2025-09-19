using Microsoft.Extensions.Localization;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Repositories;
namespace OnlineStore.Services;

public class CartItemService : ICartItemService
{
    private readonly IUnitOfWork _unitOfWorkRepo;
    private readonly IStringLocalizer<CartItemService> _localizer;
    public CartItemService(IUnitOfWork unitOfWorkRepo, IStringLocalizer<CartItemService> localizer)
    {
        _unitOfWorkRepo = unitOfWorkRepo;
        _localizer = localizer;
    }
    // add new CartItem 
    public async Task<CartItem> Add(CartItemDto cartItemDto)
    {
        // check if product exists and cart exists

        if (!cartItemDto.ProductId.HasValue || !cartItemDto.VariantId.HasValue)
            throw new ArgumentException(_localizer["ProductAndVariantRequired"]);

        // add items to the CartItem 
        var newItem = new CartItem
        {
            CartId = cartItemDto.CartId,
            ProductId = cartItemDto.ProductId.Value,
            Quantity = cartItemDto.Quantity,
            VariantId = cartItemDto.VariantId.Value

        };
        return await _unitOfWorkRepo.CartItem.AddAsync(newItem);
    }
    // add new CartItem 
    public async Task<Cart?> Update(int userId , UpdateCartDto updateCartDto)
    {
        // get cart item , check if exists
        var cartItem = await _unitOfWorkRepo.CartItem.GetByIdAsync(updateCartDto.CartItemId);
        if (cartItem == null)
            throw new NotFoundException(string.Format(_localizer["CartItemNotFound", updateCartDto.CartItemId]));

        cartItem.Quantity = updateCartDto.Quantity;
        await _unitOfWorkRepo.CartItem.UpdateAsync(cartItem);

        return await _unitOfWorkRepo.Cart.GetByUserIdAsync(userId) ;
    }
    //
    public async Task<Cart> AddToCart(int userId, AddToCartDto addToCartDto)
    {
        // if cart item ( product or product variant) exists increase quantity 
        if (!addToCartDto.ProductId.HasValue || !addToCartDto.VariantId.HasValue)
            throw new ArgumentException(_localizer["ProductAndVariantRequired"]);

        // check if product is found in database
        // check if product has variant of variant id
       var product = await _unitOfWorkRepo.Product.GetByIdAsync(addToCartDto.ProductId.Value);

        if (product == null)
            throw new NotFoundException(_localizer["ProductNotFound"]);

        var variantExists = product.ProductVariants.Any(pv => pv.Id == addToCartDto.VariantId.Value);

        if (!variantExists)
            throw new NotFoundException(_localizer["VariantNotFound"]);

        var cart = await _unitOfWorkRepo.Cart.GetByUserIdAsync(userId);

        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            await _unitOfWorkRepo.Cart.AddAsync(cart);
        }

        var cartItem = await _unitOfWorkRepo.CartItem.GetByProductIdAndVariantIdAsync(cart.Id, addToCartDto.ProductId.Value, addToCartDto.VariantId);
        if (cartItem == null)
        {
            // add 
            cartItem = new CartItem
            {
                CartId = cart.Id,
                ProductId = addToCartDto.ProductId.Value,
                VariantId = addToCartDto.VariantId.Value,
                Quantity = addToCartDto.Quantity,
            };
            await _unitOfWorkRepo.CartItem.AddAsync(cartItem);
        }
        else
        {
            cartItem.Quantity += addToCartDto.Quantity;
            await _unitOfWorkRepo.CartItem.UpdateAsync(cartItem);
        }
        return cart;
    }
    // remove CartItem
    public async Task<bool> Delete(int id)
    {
        bool status = await _unitOfWorkRepo.CartItem.DeleteAsync(id);
        if (!status)
            throw new NotFoundException(string.Format(_localizer["CartItemNotFound", id]));
        return status;
    }
    // list all CartItems
    public async Task<IEnumerable<CartItem>> ListByCart(int cartId)
    {
        return await _unitOfWorkRepo.CartItem.GetAllByCartAsync(cartId);
    }
}