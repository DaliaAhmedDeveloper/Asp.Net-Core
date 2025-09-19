namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/product-attribute-value")]
public class ProductAttributeValueController : Controller
{
    private readonly IProductAttributeValueService _productAttributeValue;
     private readonly IProductAttributeService _productAttribute;
    public ProductAttributeValueController(IProductAttributeValueService productAttributeValue, IProductAttributeService productAttribute)
    {
        _productAttributeValue = productAttributeValue;
        _productAttribute = productAttribute;
    }

    // GET: dashboard/product-attribute-value
    [HttpGet]
    [Authorize(Policy = "attributeValue.list")]
    public async Task<IActionResult> Index(int? page, string searchTxt)
    {
        int pageNumber = page ?? 1;
        var results = await _productAttributeValue.GetAllWithPaginationForWeb(searchTxt, pageNumber, 10);
        return View(results);
    }

    // GET: dashboard/product-attribute-value/create
    [HttpGet("create")]
    [Authorize(Policy = "attributeValue.add")]
    public async Task<IActionResult> Create()
    {
        ViewBag.Attributes = await _productAttribute.GetAllForWeb();
        return View();
    }

    // POST: dashboard/product-attribute-value
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "attributeValue.add")]
    public async Task<IActionResult> Create(ProductAttributeValueViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Attributes = await _productAttribute.GetAllForWeb();
            return View(model);
        }

        await _productAttributeValue.CreateForWeb(model);
        TempData["SuccessMessage"] = "Attribute value created successfully!";
        return RedirectToAction(nameof(Index));
    }

    // GET: dashboard/product-attribute-value/edit/5
    [HttpGet("edit/{id}")]
    [Authorize(Policy = "attributeValue.update")]
    public async Task<IActionResult> Edit(int id)
    {
        var value = await _productAttributeValue.GetForWeb(id);
        if (value == null)
            return NotFound();

        var model = new ProductAttributeValueViewModel
        {
            Id = value.Id,
            AttributeId = value.AttributeId,
            Code = value.Code ?? "",
            NameEn = value.Translations.FirstOrDefault(t => t.LanguageCode == "en")?.Name ?? "",
            NameAr = value.Translations.FirstOrDefault(t => t.LanguageCode == "ar")?.Name ?? ""
        };

        ViewBag.Attributes = await _productAttribute.GetAllForWeb();
        return View(model);
    }

    // POST: dashboard/product-attribute-value/edit/5
    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "attributeValue.update")]
    public async Task<IActionResult> Edit(ProductAttributeValueViewModel model, int id)
    {
        var value = await _productAttributeValue.GetForWeb(id);
        if (!ModelState.IsValid)
        {
            ViewBag.Attributes = await _productAttributeValue.GetAllForWeb();
            return View(model);
        }

        if (value == null)
            return NotFound();

        await _productAttributeValue.UpdateForWeb(model, value);
        TempData["SuccessMessage"] = "Attribute value updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    // POST: dashboard/product-attribute-value/delete/5
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "attributeValue.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        bool record = await _productAttributeValue.DeleteForWeb(id);
        if (!record)
            return NotFound();

        TempData["SuccessMessage"] = "Attribute value deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
