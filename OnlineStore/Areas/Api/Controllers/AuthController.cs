namespace OnlineStore.Areas.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.Dtos.Responses;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using OnlineStore.Services;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authentication;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Helpers;

[Route("[area]/auth")]
[ApiController]
[Area("Api")]
public class AuthController : ControllerBase
{
    private readonly IUserService _user;
    private readonly IRefreshTokenService _refreshToken;
    private readonly IStringLocalizer<AuthController> _localizer;
    public AuthController(
        IUserService user,
        IRefreshTokenService refreshToken,
        IStringLocalizer<AuthController> localizer
        )
    {
        _user = user;
        _localizer = localizer;
        _refreshToken = refreshToken;
    }

    [HttpPost("login")]
    public async Task<IActionResult> SignIn(LoginDto model)
    {
        var response = await _user.SignIn(model);
        if (response != null) return Ok(ApiResponseHelper<UserDto>.Success(response, ""));
        ModelState.AddModelError(string.Empty, _localizer["InvalidEmailOrPassword"]); // localization
        return ValidationProblem(ModelState);
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserDto model)
    {
        var user = await _user.Add(model);
        return Ok(ApiResponseHelper<UserDto>.Success(user, _localizer["SuccessPleaseLoginNow"]));
    }
    // facebook login / register
    [HttpGet("facebook")]
    public IActionResult AuthByFacebook()
    {
        var props = new AuthenticationProperties();
        return Challenge(props, "Facebook");
    }
    // google login / register
    [HttpGet("google")]
    public IActionResult AuthByGoogle()
    {
        /*
        Authentication options like redirect URL.
        if no options the middleware uses default behavior
        */
        var props = new AuthenticationProperties();

        /*
        Tells ASP.NET Core: “I want the user to authenticate using this external provider”.
        */
        return Challenge(props, "Google"); 
    }

    //[Authorize]
    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshToken(RefreshTokenDto dto)
    {
        // get refresh token
        var response = await _refreshToken.Refresh(dto.RefreshTokenString);
        if (response == null) return NotFound(ApiResponseHelper<string>.Fail(_localizer["RefreshTokenIncorrect"], 404));
        return Ok(ApiResponseHelper<UserResponseDto>.Success(response, ""));
    }

    [HttpGet("logout")]
    [Authorize(AuthenticationSchemes = "Api")]
    public async Task<IActionResult> LogOut()
    {
        await _user.LogOut(HttpContext);
        return Ok(ApiResponseHelper<string>.Success(_localizer["LoggedOutSuccessfully"]));
    }

    // extenal login url for social media
    [Route("external-login-callback")]
    public async Task<IActionResult> ExternalLoginCallback()
    {
        var response = await _user.ExternalLoginCallback(HttpContext);
        return Ok(ApiResponseHelper<UserDto>.Success(response, ""));
    }

}