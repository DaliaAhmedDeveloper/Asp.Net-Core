namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/city")]
public class CityController : Controller
{
    private readonly ICityService _city;
    private readonly IStateService _state;

    public CityController(ICityService city, IStateService state)
    {
        _city = city;
        _state = state;
    }

    // GET: dashboard/city
    [HttpGet]
    [Authorize(Policy = "city.list")]
    public async Task<IActionResult> Index(int? page, string searchTxt)
    {
        int pageNumber = page ?? 1;
        var results = await _city.GetAllWithPaginationForWeb(searchTxt, pageNumber, 10);
        return View(results);
    }

    // GET: dashboard/city/5
    [HttpGet("{id}")]
    [Authorize(Policy = "city.show")]
    public async Task<IActionResult> Details(int id)
    {
        var city = await _city.GetForWeb(id);
        if (city == null)
            return NotFound();

        var model = new CityViewModel
        {
            Id = city.Id,
            Name = city.Name,
            StateId = city.StateId,
            NameEn = city.Translations.FirstOrDefault(t => t.LanguageCode == "en")?.Name ?? "",
            NameAr = city.Translations.FirstOrDefault(t => t.LanguageCode == "ar")?.Name ?? "",
        };
        ViewBag.states = await _state.GetAllForWeb();
        return View(model);
    }

    // GET: dashboard/city/create
    [HttpGet("create")]
    [Authorize(Policy = "city.add")]
    public async Task<IActionResult> Create()
    {
        ViewBag.states = await _state.GetAllForWeb();
        return View();
    }

    // POST: dashboard/city
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "city.add")]
    public async Task<IActionResult> Create(CityViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.states = await _state.GetAllForWeb();
            return View(model);
        }
        await _city.CreateForWeb(model);
        TempData["SuccessMessage"] = "City added successfully!";
        return RedirectToAction(nameof(Index));
    }

    // GET: dashboard/city/edit/5
    [HttpGet("edit/{id}")]
    [Authorize(Policy = "city.update")]
    public async Task<IActionResult> Edit(int id)
    {
        var city = await _city.GetForWeb(id);
        if (city == null)
            return NotFound();

        var model = new CityViewModel
        {
            Id = city.Id,
            Name = city.Name,
            StateId = city.StateId,
            NameEn = city.Translations.FirstOrDefault(t => t.LanguageCode == "en")?.Name ?? "",
            NameAr = city.Translations.FirstOrDefault(t => t.LanguageCode == "ar")?.Name ?? "",
        };
        ViewBag.states = await _state.GetAllForWeb();
        return View(model);
    }

    // POST: dashboard/city/edit/5
    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "city.update")]
    public async Task<IActionResult> Edit(CityViewModel model, int id)
    {
        var city = await _city.GetForWeb(id);
        if (!ModelState.IsValid)
        {
            ViewBag.states = await _state.GetAllForWeb();
            return View(model);
        }
        if (city == null)
            return NotFound();

        await _city.UpdateForWeb(model, city);
        TempData["SuccessMessage"] = "City updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    // POST: dashboard/city/delete/5
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "city.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        bool record = await _city.DeleteForWeb(id);
        if (!record)
            return NotFound();

        TempData["SuccessMessage"] = "City deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
