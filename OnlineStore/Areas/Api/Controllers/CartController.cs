using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OnlineStore.Helpers;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Services;

namespace OnlineStore.Areas.Api.Controllers;

[Route("[area]/cart")]
[ApiController]
[Area("Api")]
[Authorize(AuthenticationSchemes = "Api")]
public class CartController : ControllerBase
{
    private readonly ICartItemService _cartItem;
    private readonly IStringLocalizer<CartController> _localizer;
    public CartController(ICartItemService cartItem, IStringLocalizer<CartController> localizer)
    {
        _cartItem = cartItem;
        _localizer = localizer;
    }
    [HttpPost]
    // add to cart 
    public async Task<IActionResult> addToCart(AddToCartDto addToCartDto)
    {
        int userId = AuthHelper.GetAuthenticatedUserId(HttpContext);
        // check if user has cart
        var cart = await _cartItem.AddToCart(userId, addToCartDto);
        return Ok(ApiResponseHelper<string>.Success("", _localizer["AddedToCartSuccessfully"]));
    }
    // update cart item
    [HttpPut]
    public async Task<IActionResult> UpdateCartItem(UpdateCartDto updateCartDto)
    {
        int userId = AuthHelper.GetAuthenticatedUserId(HttpContext);
        var cart = await _cartItem.Update(userId, updateCartDto);
        if (cart == null)
            throw new NotFoundException(_localizer["CartIsNotFound"]);

        return Ok(ApiResponseHelper<string>.Success("", _localizer["CartUpdatedSuccessfully"]));
    }

    // delete from cart
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCartItem(int id)
    {
        bool status = await _cartItem.Delete(id);
        if (!status)
            return BadRequest(ApiResponseHelper<string>.Fail(_localizer["NoItemsAffected"], 400));

        return Ok(ApiResponseHelper<string>.Success("", _localizer["ItemDeletedSuccessfully"]));
    }
}