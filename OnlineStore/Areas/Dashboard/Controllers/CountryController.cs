namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/country")]
public class CountryController : Controller
{
    private readonly ICountryService _country;
    public CountryController(ICountryService country)
    {
        _country = country;
    }
    // GET: dashboard/country
    [HttpGet]
    [Authorize(Policy = "country.list")]
    public async Task<IActionResult> Index(int? page, string searchTxt)
    {
        int pageNumber = page ?? 1;
        var results = await _country.GetAllWithPaginationForWeb(searchTxt, pageNumber, 10);
        return View(results);
    }

    // GET: dashboard/Country/5
    [HttpGet("{id}")]
    [Authorize(Policy = "country.show")]
    public async Task<IActionResult> Details(int id)
    {
        var country = await _country.GetForWeb(id);
        if (country == null)
            return NotFound();

        // Map to view model
        var model = new CountryViewModel
        {
            Id = country.Id,
            Code = country.Code,
            NameEn = country.Translations.FirstOrDefault(t => t.LanguageCode == "en")?.Name ?? "",
            NameAr = country.Translations.FirstOrDefault(t => t.LanguageCode == "ar")?.Name ?? "",
        };
        ViewBag.Parents = await _country.GetAllForWeb();
        return View(model);
    }

    // GET: dashboard/Country/create
    [HttpGet("create")]
    [Authorize(Policy = "country.add")]
    public async Task<IActionResult> Create()
    {
        // Load parent categories for dropdown if needed
        ViewBag.Parents = await _country.GetAllForWeb();
        return View();
    }

    // POST: dashboard/Country
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "country.add")]
    public async Task<IActionResult> Create(CountryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Parents = await _country.GetAllForWeb();
            return View(model);
        }
        await _country.CreateForWeb(model);

        // Set success message
        TempData["SuccessMessage"] = "Country added successfully!";

        return RedirectToAction(nameof(Index));
    }

    // GET: dashboard/Country/edit/5
    [HttpGet("edit/{id}")]
    [Authorize(Policy = "country.update")]
    public async Task<IActionResult> Edit(int id)
    {
        var country = await _country.GetForWeb(id);
        if (country == null)
            return NotFound();

        // Map to view model
        var model = new CountryViewModel
        {
            Id = country.Id,
            Code = country.Code,
            NameEn = country.Translations.FirstOrDefault(t => t.LanguageCode == "en")?.Name ?? "",
            NameAr = country.Translations.FirstOrDefault(t => t.LanguageCode == "ar")?.Name ?? "",
        };
        ViewBag.Parents = await _country.GetAllForWeb();
        return View(model);
    }

    // Post: dashboard/Country/5
    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "country.update")]
    public async Task<IActionResult> Edit(CountryViewModel model, int id)
    {
        var Country = await _country.GetForWeb(id);
        if (!ModelState.IsValid)
        {
            ViewBag.Parents = await _country.GetAllForWeb();
            return View(model);
        }
        if (Country == null)
            return NotFound();

        await _country.UpdateForWeb(model, Country);
        // Set success message
        TempData["SuccessMessage"] = "Country Updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    // : dashboard/Country/5
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "country.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        bool record = await _country.DeleteForWeb(id);
        if (!record)
            return NotFound();

        TempData["SuccessMessage"] = "Country Deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}