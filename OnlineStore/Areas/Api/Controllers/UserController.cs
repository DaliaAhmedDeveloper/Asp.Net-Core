namespace OnlineStore.Areas.Api.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OnlineStore.Helpers;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;
using ResponseDto = OnlineStore.Models.Dtos.Responses;
using OnlineStore.Services;

[Route("[area]/profile")]
[ApiController]
[Area("Api")]
[Authorize(AuthenticationSchemes = "Api")] // Only authenticated users (with valid JWT tokens) can access this controller
public class UserController : ControllerBase
{
    private readonly IUserService _user;
    private readonly IAddressService _address;

    private readonly IWishlistService _wishlist;
    private readonly ICartService _cart;
    private readonly IWalletService _wallet;
    private readonly IReviewService _review;
    private readonly IOrderService _order;
    private readonly INotificationService _notification;
     private readonly IUserPointService _userPoint;
    private readonly IStringLocalizer<UserController> _localizer;

    public UserController(
        IReviewService review,
        IOrderService order,
        IUserService user,
        IAddressService address,
        ICartService cart,
        INotificationService notification,
        IWalletService wallet,
        IWishlistService wishlist,
         IUserPointService userPoint,
        IStringLocalizer<UserController> localizer
        )
    {
        _user = user;
        _address = address;
        _cart = cart;
        _notification = notification;
        _wallet = wallet;
        _wishlist = wishlist;
        _review = review;
        _order = order;
        _localizer = localizer;
        _userPoint = userPoint;
    }

    // Get the profile information of the currently authenticated user
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var id = AuthHelper.GetAuthenticatedUserId(HttpContext);
        var user = await _user.Find(id);
        return Ok(ApiResponseHelper<ResponseDto.UserResponseDto>.Success(ResponseDto.UserResponseDto.FromModel(user), ""));
    }

    // Update user profile - implementation pending
    [HttpPost]
    public async Task<IActionResult> Update(UpdateUserDto userDto)
    {
        var id = AuthHelper.GetAuthenticatedUserId(HttpContext);
        var user = await _user.UpdateProfile(id, userDto);
        if (user == null)
            throw new NotFoundException(_localizer["UserNotFound"]);

        return Ok(ApiResponseHelper<ResponseDto.UserResponseDto>.Success(ResponseDto.UserResponseDto.FromModel(user), _localizer["ProfileUpdatedSuccessfully"]));
    }

    // Change user password - implementation pending
    [HttpPost("changePassword")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var id = AuthHelper.GetAuthenticatedUserId(HttpContext);
        bool status = await _user.ChangePasswordAsync(id, changePasswordDto);
        if (!status)
            throw new NotFoundException(_localizer["UserNotFound"]);

        return Ok(ApiResponseHelper<string>.Success("", _localizer["PasswordUpdatedSuccessfully"]));
    }
    // Get all addresses associated with the authenticated user
    [HttpGet("addresses")]
    public async Task<IActionResult> Addresses()
    {
        var id = AuthHelper.GetAuthenticatedUserId(HttpContext);
        var addresses = await _address.ListByUser(id);
        return Ok(ApiResponseHelper<Address>.CollectionSuccess(addresses, ""));
    }

    // Get all orders of the authenticated user
    [HttpGet("orders")]
    public async Task<IActionResult> Orders()
    {
        var id = AuthHelper.GetAuthenticatedUserId(HttpContext);
        var orders = await _order.ListAllByUser(id);
        return Ok(ApiResponseHelper<ResponseDto.OrderListDto>.CollectionSuccess(orders, ""));
    }
    // Get wishlist items for the authenticated user
    [HttpGet("wishlist")]
    public async Task<IActionResult> Wishlist()
    {
        var id = AuthHelper.GetAuthenticatedUserId(HttpContext);
        var wishlist = await _wishlist.ListByUser(id);
        return Ok(ApiResponseHelper<ResponseDto.WishlistDto>.CollectionSuccess(wishlist, ""));
    }

    // Get cart items for the authenticated user
    [HttpGet("cart")]
    public async Task<IActionResult> Cart()
    {
        var id = AuthHelper.GetAuthenticatedUserId(HttpContext);
        var cart = await _cart.FindDetailsByUserId(id);
        if (cart == null)
            throw new NotFoundException(_localizer["CartNotFound"]);

        return Ok(ApiResponseHelper<ResponseDto.CartDto>.Success(cart, ""));
    }

    // Get wallet info of the authenticated user
    [HttpGet("wallet")]
    public async Task<IActionResult> Wallet()
    {
        var id = AuthHelper.GetAuthenticatedUserId(HttpContext);
        var wallet = await _wallet.FindByUserId(id);
        if (wallet == null)
            throw new NotFoundException(_localizer["WalletNotFound"]);

        return Ok(ApiResponseHelper<Wallet>.Success(wallet, ""));
    }

    // Get notifications for the authenticated user
    [HttpGet("notifications")]
    public async Task<IActionResult> Notifications()
    {
        var id = AuthHelper.GetAuthenticatedUserId(HttpContext);
        var notifications = await _notification.ListByUser(id);
        return Ok(ApiResponseHelper<ResponseDto.NotificationDto>.CollectionSuccess(notifications, ""));
    }

    // Get user reviews made by the authenticated user
    [HttpGet("reviews")]
    public async Task<IActionResult> Reviews()
    {
        var id = AuthHelper.GetAuthenticatedUserId(HttpContext);
        var reviews = await _review.ListByUser(id);
        return Ok(ApiResponseHelper<ResponseDto.ReviewDto>.CollectionSuccess(reviews, ""));
    }

    // Login everyday to earn points 
    [HttpGet("clickToEarn")]
    public async Task<IActionResult> LoginToEarn()
    {
        var id = AuthHelper.GetAuthenticatedUserId(HttpContext);
        var points = await _userPoint.Add(id);
        return Ok(ApiResponseHelper<UserPoint>.Success(points, ""));
    }
    // Recently viewed products	
}
