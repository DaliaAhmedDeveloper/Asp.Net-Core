namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/tag")]
public class TagController : Controller
{
    private readonly ITagService _tag;
    public TagController(ITagService tag)
    {
        _tag = tag;
    }

    // GET: dashboard/tag
    [HttpGet]
    [Authorize(Policy = "tag.list")]
    public async Task<IActionResult> Index(int? page, string searchTxt)
    {
        int pageNumber = page ?? 1;
        var results = await _tag.GetAllWithPaginationForWeb(searchTxt, pageNumber, 10);
        return View(results);
    }

    // GET: dashboard/tag/create
    [HttpGet("create")]
    [Authorize(Policy = "tag.add")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: dashboard/tag
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "tag.add")]
    public async Task<IActionResult> Create(TagViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _tag.CreateForWeb(model);
        TempData["SuccessMessage"] = "Tag added successfully!";
        return RedirectToAction(nameof(Index));
    }

    // GET: dashboard/tag/edit/5
    [HttpGet("edit/{id}")]
    [Authorize(Policy = "tag.update")]
    public async Task<IActionResult> Edit(int id)
    {
        var tag = await _tag.GetForWeb(id);
        if (tag == null)
            return NotFound();

        var model = new TagViewModel
        {
            Id = tag.Id,
            Code = tag.Code ?? "",
            NameEn = tag.Translations.FirstOrDefault(t => t.LanguageCode == "en")?.Name ?? "",
            NameAr = tag.Translations.FirstOrDefault(t => t.LanguageCode == "ar")?.Name ?? "",

        };
        return View(model);
    }

    // POST: dashboard/tag/5
    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "tag.update")]
    public async Task<IActionResult> Edit(TagViewModel model, int id)
    {
        var tag = await _tag.GetForWeb(id);
        if (!ModelState.IsValid)
            return View(model);

        if (tag == null)
            return NotFound();

        await _tag.UpdateForWeb(model, tag);
        TempData["SuccessMessage"] = "Tag updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    // POST: dashboard/tag/delete/5
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "tag.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        bool record = await _tag.DeleteForWeb(id);
        if (!record)
            return NotFound();

        TempData["SuccessMessage"] = "Tag deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
