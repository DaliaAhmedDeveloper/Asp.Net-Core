namespace OnlineStore.Areas.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Services;
using OnlineStore.Helpers;
using OnlineStore.Models.Dtos.Responses;

[Area("Api")]
[Authorize(AuthenticationSchemes = "Api")]
[Route("[area]/shippingMethods")]
public class ShippingMethodController : ControllerBase
{
    private readonly IShippingMethodService _method;
    public ShippingMethodController(IShippingMethodService method)
    {
        _method = method;
    }
    // list
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var methods = await _method.ListForApi();
        return Ok(ApiResponseHelper<ShippingMethodDto>.CollectionSuccess(methods, ""));
    }
}