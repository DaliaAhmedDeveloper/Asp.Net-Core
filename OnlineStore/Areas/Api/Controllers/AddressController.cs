namespace OnlineStore.Areas.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Services;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Models;
using OnlineStore.Helpers;
using Microsoft.Extensions.Localization;

[Area("Api")]
[Authorize(AuthenticationSchemes = "Api")]
[ApiController]
[Route("[area]/address")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _address;
    private readonly IStringLocalizer<AddressController> _localizer;

    public AddressController(IAddressService address, IStringLocalizer<AddressController> localizer)
    {
        _address = address;
        _localizer = localizer;
    }
    // add new address
    [HttpPost]
    public async Task<IActionResult> Add(CreateAddressDto CreateAddressDto)
    {
        var userId = AuthHelper.GetAuthenticatedUserId(HttpContext);
        var address = await _address.Add( userId ,CreateAddressDto);
        return Ok(ApiResponseHelper<Address>.Success(address, _localizer["AddressAddedSuccessfully"]));
    }
    // update address 
    [HttpPut]
    public async Task<IActionResult> Update(UpdateAddressDto updateAddressDto)
    {
        var userId = AuthHelper.GetAuthenticatedUserId(HttpContext);
        var address = await _address.Update(userId , updateAddressDto);
        if (address == null)
            throw new NotFoundException(_localizer["AddressNotFound"]);

        return Ok(ApiResponseHelper<Address>.Success(address, _localizer["AddressUpdatedSuccessfully"]));
    }
    // make default
    [HttpPost("{id}/makeItDefault")]
    public async Task<IActionResult> MakeItDefault(int id)
    {

        var address = await _address.MakeItDefault(id);
        // make all other address not default
        return Ok(ApiResponseHelper<Address>.Success(address, _localizer["AddressMarkedAsDefault"]));
    }
    // delete address
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var address = await _address.Delete(id);
        return Ok(ApiResponseHelper<string>.Success("", _localizer["AddressDeletedSuccessfully"]));
    }

}