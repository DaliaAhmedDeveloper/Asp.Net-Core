namespace OnlineStore.Areas.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Services;
using OnlineStore.Helpers;
using Microsoft.Extensions.Localization;
using OnlineStore.Models.Dtos.Responses;

[Area("Api")]
[Authorize(AuthenticationSchemes = "Api")]
[Route("[area]/wishlist")]
public class WishlistController : ControllerBase
{
    private readonly IWishlistService _wishlist;
   private readonly IStringLocalizer<WishlistController> _localizer;
    public WishlistController(IWishlistService wishlist , IStringLocalizer<WishlistController> localizer)
    {
        _localizer = localizer;
        _wishlist = wishlist;
    }
    // list
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var userId = AuthHelper.GetAuthenticatedUserId(HttpContext);
        var wishlist = await _wishlist.ListByUser(userId);
        return Ok(ApiResponseHelper<WishlistDto>.CollectionSuccess(wishlist, ""));
    }
    // add new Wishlist
    [HttpPost("{productId}")]
    public async Task<IActionResult> Add(int productId)
    {
        var id = AuthHelper.GetAuthenticatedUserId(HttpContext);
        var result = await _wishlist.Add(id, productId);
        if (result == null)
            return BadRequest(ApiResponseHelper<string>.Fail(_localizer["ProductAlreadyExists"], 400));

        var wishlist = await _wishlist.ListByUser(id);
        return Ok(ApiResponseHelper<WishlistDto>.CollectionSuccess(wishlist, _localizer["ProductAddedSuccessfully"]));
    }
    // delete Wishlist
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = AuthHelper.GetAuthenticatedUserId(HttpContext);
        await _wishlist.Delete(id);
        var wishlist = await _wishlist.ListByUser(userId);
        return Ok(ApiResponseHelper<WishlistDto>.CollectionSuccess(wishlist, _localizer["ProductRemovedSuccessfully"]));
    }

}