namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/product-attribute")]
public class ProductAttributeController : Controller
{
    private readonly IProductAttributeService _productAttribute;
    public ProductAttributeController(IProductAttributeService productAttribute)
    {
        _productAttribute = productAttribute;
    }

    // GET: dashboard/product-attribute
    [HttpGet]
    [Authorize(Policy = "attribute.list")]
    public async Task<IActionResult> Index(int? page, string searchTxt)
    {
        int pageNumber = page ?? 1;
        var results = await _productAttribute.GetAllWithPaginationForWeb(searchTxt, pageNumber, 10);
        return View(results);
    }

    // GET: dashboard/product-attribute/5
    [HttpGet("{id}")]
    [Authorize(Policy = "attribute.show")]
    public async Task<IActionResult> Details(int id)
    {
        var attribute = await _productAttribute.GetForWeb(id);
        if (attribute == null)
            return NotFound();

        var model = new ProductAttributeViewModel
        {
            Id = attribute.Id,
            Code = attribute.Code ?? "",
            NameEn = attribute.Translations.FirstOrDefault(t => t.LanguageCode == "en")?.Name ?? "",
            NameAr = attribute.Translations.FirstOrDefault(t => t.LanguageCode == "ar")?.Name ?? ""
        };
        return View(model);
    }

    // GET: dashboard/product-attribute/create
    [HttpGet("create")]
    [Authorize(Policy = "attribute.add")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: dashboard/product-attribute
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "attribute.add")]
    public async Task<IActionResult> Create(ProductAttributeViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _productAttribute.CreateForWeb(model);
        TempData["SuccessMessage"] = "Attribute created successfully!";
        return RedirectToAction(nameof(Index));
    }

    // GET: dashboard/product-attribute/edit/5
    [HttpGet("edit/{id}")]
    [Authorize(Policy = "attribute.update")]
    public async Task<IActionResult> Edit(int id)
    {
        var attribute = await _productAttribute.GetForWeb(id);
        if (attribute == null)
            return NotFound();

        var model = new ProductAttributeViewModel
        {
            Id = attribute.Id,
            Code = attribute.Code ?? "",
            NameEn = attribute.Translations.FirstOrDefault(t => t.LanguageCode == "en")?.Name ?? "",
            NameAr = attribute.Translations.FirstOrDefault(t => t.LanguageCode == "ar")?.Name ?? ""
        };
        return View(model);
    }

    // POST: dashboard/product-attribute/edit/5
    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "attribute.update")]
    public async Task<IActionResult> Edit(ProductAttributeViewModel model, int id)
    {
        var attribute = await _productAttribute.GetForWeb(id);
        if (!ModelState.IsValid)
            return View(model);

        if (attribute == null)
            return NotFound();

        await _productAttribute.UpdateForWeb(model, attribute);
        TempData["SuccessMessage"] = "Attribute updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    // POST: dashboard/product-attribute/delete/5
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "attribute.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        bool record = await _productAttribute.DeleteForWeb(id);
        if (!record)
            return NotFound();

        TempData["SuccessMessage"] = "Attribute deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
    
    // get attribute values
    [HttpGet("{attributeId}/values")]
    public async Task<IActionResult> GetValuesByAttribute(int attributeId)
    {
        var values = await _productAttribute.GetValuesForWeb(attributeId);
        return Ok(values);
    }
}
