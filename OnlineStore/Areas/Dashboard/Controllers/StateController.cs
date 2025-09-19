namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/state")]
public class StateController : Controller
{
    private readonly IStateService _state;
    private readonly ICountryService _country;
    public StateController(IStateService state, ICountryService country)
    {
        _state = state;
        _country = country;
    }

    // GET: dashboard/state
    [HttpGet]
    [Authorize(Policy = "state.list")]
    public async Task<IActionResult> Index(int? page, string searchTxt)
    {
        int pageNumber = page ?? 1;
        var results = await _state.GetAllWithPaginationForWeb(searchTxt, pageNumber, 10);
        return View(results);
    }

    // GET: dashboard/state/5
    [HttpGet("{id}")]
    [Authorize(Policy = "state.show")]
    public async Task<IActionResult> Details(int id)
    {
        var state = await _state.GetForWeb(id);
        if (state == null)
            return NotFound();

        var model = new StateViewModel
        {
            Id = state.Id,
            Code = state.Code,
            CountryId = state.CountryId,
            NameEn = state.Translations.FirstOrDefault(t => t.LanguageCode == "en")?.Name ?? "",
            NameAr = state.Translations.FirstOrDefault(t => t.LanguageCode == "ar")?.Name ?? "",
        };
        ViewBag.countries = await _country.GetAllForWeb();
        return View(model);
    }

    // GET: dashboard/state/create
    [HttpGet("create")]
    [Authorize(Policy = "state.add")]
    public async Task<IActionResult> Create()
    {
        ViewBag.countries = await _country.GetAllForWeb();
        return View();
    }

    // POST: dashboard/state/create
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "state.add")]
    public async Task<IActionResult> Create(StateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.countries = await _country.GetAllForWeb();
            return View(model);
        }
        await _state.CreateForWeb(model);

        TempData["SuccessMessage"] = "State added successfully!";
        return RedirectToAction(nameof(Index));
    }

    // GET: dashboard/state/edit/5
    [HttpGet("edit/{id}")]
    [Authorize(Policy = "state.update")]
    public async Task<IActionResult> Edit(int id)
    {
        var state = await _state.GetForWeb(id);
        if (state == null)
            return NotFound();

        var model = new StateViewModel
        {
            Id = state.Id,
            Code = state.Code,
            CountryId = state.CountryId,
            NameEn = state.Translations.FirstOrDefault(t => t.LanguageCode == "en")?.Name ?? "",
            NameAr = state.Translations.FirstOrDefault(t => t.LanguageCode == "ar")?.Name ?? "",
        };
        ViewBag.countries = await _country.GetAllForWeb();
        return View(model);
    }

    // POST: dashboard/state/edit/5
    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "state.update")]
    public async Task<IActionResult> Edit(StateViewModel model, int id)
    {
        var state = await _state.GetForWeb(id);
        if (!ModelState.IsValid)
        {
            ViewBag.countries = await _country.GetAllForWeb();
            return View(model);
        }
        if (state == null)
            return NotFound();

        await _state.UpdateForWeb(model, state);
        TempData["SuccessMessage"] = "State updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    // POST: dashboard/state/delete/5
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "state.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        bool record = await _state.DeleteForWeb(id);
        if (!record)
            return NotFound();

        TempData["SuccessMessage"] = "State deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
