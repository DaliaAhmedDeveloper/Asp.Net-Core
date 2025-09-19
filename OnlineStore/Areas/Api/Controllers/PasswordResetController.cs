namespace OnlineStore.Areas.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Services;
using OnlineStore.Models.Dtos.Requests;
using Microsoft.Extensions.Localization;
using OnlineStore.Helpers;

[Area("Api")]
[Authorize(AuthenticationSchemes = "Api")]
[ApiController]
[Route("[area]/passwordReset")]
public class PasswordResetController : ControllerBase
{
    private readonly IPasswordResetService _passwordReset;
    private readonly IStringLocalizer<PasswordResetController> _localizer;

    public PasswordResetController(IPasswordResetService passwordReset, IStringLocalizer<PasswordResetController> localizer)
    {
        _passwordReset = passwordReset;
        _localizer = localizer;
    }
    // send email request
    [HttpPost]
    public async Task<IActionResult> ResetRequest(string email)
    {
        await _passwordReset.RequestPasswordResetAsync(email);
        return Ok(ApiResponseHelper<string>.Success( "", _localizer["ResetLinkSent"]));
    }
    // update password
    [HttpPut]
    public async Task<IActionResult> ResetPassword(PasswordResetDto dto)
    {
        await _passwordReset.ResetPasswordAsync(dto);
        return Ok(ApiResponseHelper<string>.Success( "", _localizer["PasswordUpdatedSuccessfully"]));
    }
}