using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OnlineStore.Helpers;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Services;

namespace OnlineStore.Areas.Api.Controllers;

[Authorize(AuthenticationSchemes = "Api")]
[Area("Api")]
[Route("[area]/order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _order;
    private readonly IStringLocalizer<OrderController> _localizer;

    public OrderController(IOrderService order, IStringLocalizer<OrderController> localizer)
    {
        _order = order;
        _localizer = localizer;
    }

    // place order 
    [HttpPost]
    public async Task<IActionResult> PlaceOrder(CreateOrderDto dto)
    {
        int userId = AuthHelper.GetAuthenticatedUserId(HttpContext);
        var order = await _order.Create(dto, userId);
        return Ok(ApiResponseHelper<OrderDto>.Success(order, _localizer["OrderAddedSuccess"]));
    }
    // order details  
    [HttpGet("{orderId}")]
    public async Task<IActionResult> Details(int orderId)
    {
        var order = await _order.Find(orderId);
        return Ok(ApiResponseHelper<OrderDetailsDto?>.Success(order, ""));
    }
    // cancell order 
    [HttpPost("{orderId}")]
    public async Task<IActionResult> CancelOrder(int orderId)
    {
        await _order.CancelOrder(orderId);
        return Ok(ApiResponseHelper<string>.Success("", _localizer["OrderCancelledSuccess"]));
    }
    // Track order 
    [HttpGet("track/{orderNumber}")]
    public async Task<IActionResult> TrackOrder(string orderNumber)
    {
        var tracking = await _order.TrackOrder(orderNumber);
        return Ok(ApiResponseHelper<OrderTrackingDto>.Success(tracking, _localizer["OrderTrackingInfo"]));
    }
    // return 
    public async Task<IActionResult> Return(int orderId)
    {
        await _order.CancelOrder(orderId);
        return Ok(ApiResponseHelper<string>.Success("", _localizer["OrderCancelledSuccess"]));
    }
    //
    public async Task<IActionResult> ReturnTracking(int orderId)
    {
        await _order.CancelOrder(orderId);
        return Ok(ApiResponseHelper<string>.Success("", _localizer["OrderCancelledSuccess"]));
    }

}