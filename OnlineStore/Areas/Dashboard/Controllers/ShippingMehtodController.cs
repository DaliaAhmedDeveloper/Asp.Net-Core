namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/shippingMethod")]
public class ShippingMethodController : Controller
{
    private readonly IShippingMethodService _ShippingMethod;
    public ShippingMethodController(IShippingMethodService ShippingMethod)
    {
        _ShippingMethod = ShippingMethod;
    }

    // GET: dashboard/ShippingMethod
    [HttpGet]
    [Authorize(Policy = "shippingMethod.list")]
    public async Task<IActionResult> Index(int? page, string searchTxt)
    {
        int pageNumber = page ?? 1;
        var results = await _ShippingMethod.GetAllWithPaginationForWeb(searchTxt, pageNumber, 10);
        return View(results);
    }

    // GET: dashboard/ShippingMethod/create
    [HttpGet("create")]
    [Authorize(Policy = "shippingMethod.add")]
    public ActionResult Create()
    {
        return View();
    }

    // POST: dashboard/ShippingMethod
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "shippingMethod.add")]
    public async Task<IActionResult> Create(ShippingMethodViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _ShippingMethod.CreateForWeb(model);
        TempData["SuccessMessage"] = "ShippingMethod added successfully!";
        return RedirectToAction(nameof(Index));
    }

    // GET: dashboard/ShippingMethod/edit/5
    [HttpGet("edit/{id}")]
    [Authorize(Policy = "shippingMethod.update")]
    public async Task<IActionResult> Edit(int id)
    {
        var ShippingMethod = await _ShippingMethod.GetForWeb(id);
        if (ShippingMethod == null)
            return NotFound();

        var model = new ShippingMethodViewModel
        {
            Id = ShippingMethod.Id,
            Name = ShippingMethod.Name,
            Cost = ShippingMethod.Cost,
            DeliveryTime = ShippingMethod.DeliveryTime,
            NameEn = ShippingMethod.Translations.FirstOrDefault(t => t.LanguageCode == "en")?.Name ?? "",
            NameAr = ShippingMethod.Translations.FirstOrDefault(t => t.LanguageCode == "ar")?.Name ?? "",
        };
        return View(model);
    }

    // POST: dashboard/ShippingMethod/edit/5
    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "shippingMethod.update")]
    public async Task<IActionResult> Edit(ShippingMethodViewModel model, int id)
    {
        var ShippingMethod = await _ShippingMethod.GetForWeb(id);
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        if (ShippingMethod == null)
            return NotFound();

        await _ShippingMethod.UpdateForWeb(model, ShippingMethod);
        TempData["SuccessMessage"] = "ShippingMethod updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    // POST: dashboard/ShippingMethod/delete/5
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "shippingMethod.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        bool record = await _ShippingMethod.DeleteForWeb(id);
        if (!record)
            return NotFound();

        TempData["SuccessMessage"] = "ShippingMethod deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
