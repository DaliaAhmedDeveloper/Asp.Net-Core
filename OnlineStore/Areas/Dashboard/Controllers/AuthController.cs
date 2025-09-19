namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using OnlineStore.Models.Dtos.Requests;
// using Microsoft.AspNetCore.Identity;
using OnlineStore.Models.Enums;
using OnlineStore.Services;

[Area("Dashboard")]
[Route("[area]/auth")]
public class AuthController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserService _user;

    public AuthController(ILogger<HomeController> logger, IUserService user)
    {
        _logger = logger;
        _user = user;

    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> SignIn(LoginDto model)
    {

        if (!ModelState.IsValid) // if errors return login view
        {
            return View("Login", model);
        }
        // check user from data base 
        // check also if its role is admin 
        User? user = await _user.CheckUser(model.Email, model.Password , UserType.Admin);
        if (user != null)
        {
            // Create claims: a list of user-related information to store in the authentication cookie
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, model.Email),
                new Claim("UserType",  UserType.Admin.ToString())
            };

            if (user?.Roles != null)
            {
                foreach (var role in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Slug));

                    foreach (var permission in role.Permissions.Select(rp => rp.Slug))
                    {
                        claims.Add(new Claim("Permission", permission));
                    }
                }
            }
            // Create identity from the claims, specify the authentication scheme
            // // This also provides properties like IsAuthenticated , Role and Email
            var identity = new ClaimsIdentity(claims, "AdminAuth");

            // In ASP.NET, a user can have multiple identities (e.g., one from Google, one from your system)
            // // So we wrap one or more identities inside a ClaimsPrincipal, which represents the authenticated user
            var principal = new ClaimsPrincipal(identity);

            // Sign in the user by writing the claims into an encrypted cookie
            // This cookie will be sent to the user's browser and used in future requests for authentication
           await HttpContext.SignInAsync("AdminAuth", principal);
            return RedirectToAction("Index", "Home");
        }
        ModelState.AddModelError("", "Invalid Email Or Password !");
        return View("login");
    }
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("AdminAuth");
        return RedirectToAction("Login");
    }
    [HttpGet("access-denied")]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
