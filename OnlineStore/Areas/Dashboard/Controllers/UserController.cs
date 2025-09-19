namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.Enums;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/user")]
public class UserController : Controller
{
    private readonly IUserService _user;
    private readonly ICountryService _country;
    private readonly IStateService _state;
    private readonly ICityService _city;

    public UserController(IUserService user, ICountryService country, IStateService state, ICityService city)
    {
        _user = user;
        _state = state;
        _city = city;
        _country = country;
    }

    // GET: dashboard/user
    [HttpGet]
    [Authorize(Policy = "user.list")]
    public async Task<IActionResult> Index(int? page, string searchTxt)
    {
        int pageNumber = page ?? 1;
        var results = await _user.GetAllWithPaginationForWeb(searchTxt, UserType.User, pageNumber, 10);
        ViewBag.Title = "Users";
        return View(results);
    }
    // GET: dashboard/user
    [HttpGet("admins")]
    [Authorize(Policy = "user.list")]
    public async Task<IActionResult> Admins(int? page, string searchTxt)
    {
        int pageNumber = page ?? 1;
        var results = await _user.GetAllWithPaginationForWeb(searchTxt, UserType.Admin, pageNumber, 10);
        ViewBag.Title = "Admins";
        return View("Index", results);
    }
    // GET: dashboard/user/5
    [HttpGet("{id}")]
    [Authorize(Policy = "user.show")]
    public async Task<IActionResult> Details(int id)
    {
        var user = await _user.GetForWeb(id);
        if (user == null)
            return NotFound();

        var model = new UserViewModel
        {
            Id = user.Id,
            FullName = user.FullName ?? "",
            Email = user.Email ?? "",
            UserType = user.UserType,
            Provider = user.Provider ?? ProviderType.Web,
            PhoneNumber = user.PhoneNumber ?? "",
            IsActive = user.IsActive,
            CountryId = user.CountryId,
            StateId = user.StateId,
            CityId = user.CityId,
            CityName = user.City.Name,
            CountryName = user.Country.Code,
            StateName = user.State.Code,
        };
        return View(model);
    }

    // GET: dashboard/user/create
    [HttpGet("create")]
    [Authorize(Policy = "user.add")]
    public async Task<IActionResult> Create()
    {
        ViewBag.countries = await _country.GetAllForWeb();
        return View();
    }

    // POST: dashboard/user
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "user.add")]
    public async Task<IActionResult> Create(UserViewModel model)
    {
        // check if email exists
        if (!ModelState.IsValid)
        {
            ViewBag.states = await _state.GetAllForWeb();
            ViewBag.cities = await _city.GetAllForWeb();
            ViewBag.countries = await _country.GetAllForWeb();
            return View(model);
        }
        await _user.CreateForWeb(model);
        TempData["SuccessMessage"] = "user added successfully!";
        return RedirectToAction(nameof(Index));
    }

    // GET: dashboard/user/edit/5
    [HttpGet("edit/{id}")]
    [Authorize(Policy = "user.update")]
    public async Task<IActionResult> Edit(int id)
    {
        var user = await _user.GetForWeb(id);
        if (user == null)
            return NotFound();

        var model = new UpdateUserViewModel
        {
            Id = user.Id,
            FullName = user.FullName ?? "",
            Email = user.Email ?? "",
            PhoneNumber = user.PhoneNumber ?? "",
            IsActive = user.IsActive,
            CountryId = user.CountryId,
            StateId = user.StateId,
            CityId = user.CityId,
        };
        ViewBag.states = await _state.ListByCountry(user.CountryId);
        ViewBag.cities = await _city.ListByState(user.StateId);
        ViewBag.countries = await _country.GetAllForWeb();
        return View(model);
    }

    // POST: dashboard/user/edit/5
    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "user.update")]
    public async Task<IActionResult> Edit(UpdateUserViewModel model, int id)
    {
        var user = await _user.GetForWeb(id);
        if (user == null)
            return NotFound();

        if (!ModelState.IsValid)
        {
            ViewBag.states = await _state.ListByCountry(user.CountryId);
            ViewBag.cities = await _city.ListByState(user.StateId);
            ViewBag.countries = await _country.GetAllForWeb();
            return View(model);
        }

        await _user.UpdateForWeb(model, user);
        TempData["SuccessMessage"] = "user updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    // POST: dashboard/user/delete/5
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "user.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        bool record = await _user.DeleteForWeb(id);
        if (!record)
            return NotFound();

        TempData["SuccessMessage"] = "user deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
