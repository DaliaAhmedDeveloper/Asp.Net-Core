namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/category")]
public class CategoryController : Controller
{
    private readonly ICategoryService _category;
    public CategoryController(ICategoryService category)
    {
        _category = category;
    }

    // GET: dashboard/category
    [HttpGet]
    [Authorize(Policy = "category.list")]
    public async Task<IActionResult> Index(int? page, string searchTxt)
    {
        int pageNumber = page ?? 1;
        var categories = await _category.GetAllWithPaginationForWeb(searchTxt, pageNumber, 10);
        return View(categories);
    }

    // GET: dashboard/category/5
    [HttpGet("{id}")]
    [Authorize(Policy = "category.show")]
    public async Task<IActionResult> Details(int id)
    {
        var category = await _category.GetForWeb(id);
        if (category == null)
            return NotFound();

        // Map to view model
        var model = new CategoryViewModel
        {
            Id = category.Id,
            Slug = category.Slug,
            ParentId = category.ParentId,
            IsDeal = category.IsDeal,
            ImageUrl = category.ImageUrl,
            NameEn = category.Translations.FirstOrDefault(t => t.LanguageCode == "en")?.Name ?? "",
            DescriptionEn = category.Translations.FirstOrDefault(t => t.LanguageCode == "en")?.Description ?? "",
            NameAr = category.Translations.FirstOrDefault(t => t.LanguageCode == "ar")?.Name ?? "",
            DescriptionAr = category.Translations.FirstOrDefault(t => t.LanguageCode == "ar")?.Description ?? ""
        };
        ViewBag.Parents = await _category.GetAllForWeb();
        return View(model);
    }

    // GET: dashboard/category/create
    [HttpGet("create")]
    [Authorize(Policy = "category.add")]
    public async Task<IActionResult> Create()
    {
        // Load parent categories for dropdown if needed
        ViewBag.Parents = await _category.GetAllForWeb();
        return View();
    }

    // POST: dashboard/category
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "category.add")]
    public async Task<IActionResult> Create(CategoryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Parents = await _category.GetAllForWeb();
            return View(model);
        }
        await _category.CreateForWeb(model);

        // Set success message
        TempData["SuccessMessage"] = "Category added successfully!";

        return RedirectToAction(nameof(Index));
    }

    // GET: dashboard/category/edit/5
    [HttpGet("edit/{id}")]
    [Authorize(Policy = "category.update")]
    public async Task<IActionResult> Edit(int id)
    {
        var category = await _category.GetForWeb(id);
        if (category == null)
            return NotFound();

        // Map to view model
        var model = new CategoryViewModel
        {
            Id = category.Id,
            Slug = category.Slug,
            ParentId = category.ParentId,
            IsDeal = category.IsDeal,
            ImageUrl = category.ImageUrl,
            NameEn = category.Translations.FirstOrDefault(t => t.LanguageCode == "en")?.Name ?? "",
            DescriptionEn = category.Translations.FirstOrDefault(t => t.LanguageCode == "en")?.Description ?? "",
            NameAr = category.Translations.FirstOrDefault(t => t.LanguageCode == "ar")?.Name ?? "",
            DescriptionAr = category.Translations.FirstOrDefault(t => t.LanguageCode == "ar")?.Description ?? ""
        };
        ViewBag.Parents = await _category.GetAllForWeb();
        return View(model);
    }

    // Post: dashboard/category/5
    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "category.update")]
    public async Task<IActionResult> Edit(CategoryViewModel model, int id)
    {
        var category = await _category.GetForWeb(id);
        if (!ModelState.IsValid)
        {
            model.ImageUrl = category?.ImageUrl;
            ViewBag.Parents = await _category.GetAllForWeb();
            return View(model);
        }
        if (category == null)
            return NotFound();

        await _category.UpdateForWeb(model, category);
        // Set success message
        TempData["SuccessMessage"] = "Category Updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    // : dashboard/category/5
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "category.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        bool record = await _category.DeleteForWeb(id);
        if (!record)
            return NotFound();

        TempData["SuccessMessage"] = "Category Deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
