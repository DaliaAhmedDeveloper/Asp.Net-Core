namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/product")]
public class ProductController : Controller
{
    private readonly IProductService _product;
    private readonly ICategoryService _category;
    private readonly ITagService _tag;
    private readonly IWarehouseService _warehouse;
    private readonly IProductAttributeService _attribute;
    public ProductController(
        IProductService product,
        ICategoryService category,
        ITagService tag,
        IProductAttributeService attribute,
        IWarehouseService warehouse
        )
    {
        _product = product;
        _category = category;
        _tag = tag;
        _attribute = attribute;
        _warehouse = warehouse;
    }
    // GET: dashboard/product
    [HttpGet]
    [Authorize(Policy = "product.list")]
    public async Task<IActionResult> Index(int? page, string searchTxt)
    {
        int pageNumber = page ?? 1;
        var results = await _product.GetAllWithPaginationForWeb(searchTxt, pageNumber, 10);
        return View(results);
    }
    // GET: dashboard/product/5
    [HttpGet("{id}")]
    [Authorize(Policy = "product.show")]
    public async Task<IActionResult> Details(int id)
    {
        var product = await _product.GetWithRelationsForWeb(id);
        if (product == null)
            return NotFound();

        // categories
        ViewBag.categories = await _category.GetAllForWeb();
        // tags
        ViewBag.tags = await _tag.GetAllForWeb();
        // attributes
        ViewBag.attributes = await _attribute.GetAllForWeb();
        // values based on choosen attruibute 
        // warehouses
        ViewBag.warehouses = await _warehouse.GetAllForWeb();

        var variant = product.ProductVariants.FirstOrDefault();
        var vav = variant?.VariantAttributeValues.FirstOrDefault() ?? null;
        ViewBag.values = new List<AttributeValue>();

        if (variant != null && vav != null)
        {
            ViewBag.values = await _attribute.GetValuesForWeb(vav.AttributeId);
        }
        // Map Product entity -> ProductViewModel
        var model = _product.MapModel(product);
        return View(model);
    }
    // GET: dashboard/product/create
    [HttpGet("create")]
    [Authorize(Policy = "product.add")]
    public async Task<IActionResult> Create()
    {
        // categories
        ViewBag.categories = await _category.GetAllForWeb();
        // tags
        ViewBag.tags = await _tag.GetAllForWeb();
        // attributes
        ViewBag.attributes = await _attribute.GetAllForWeb();
        // values based on choosen attruibute 
        // warehouses
        ViewBag.warehouses = await _warehouse.GetAllForWeb();
        return View();
    }

    // POST: dashboard/product
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "product.add")]
    public async Task<IActionResult> Create(ProductViewModel model)
    {
        if (await _product.CheckSku(model.SKU))
        {
            ModelState.AddModelError("SKU", "SKU must be unique.");
        }
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _product.CreateForWeb(model);
        TempData["SuccessMessage"] = "Product added successfully!";
        return RedirectToAction(nameof(Index));
    }

    // GET: dashboard/product/edit/5
    [HttpGet("edit/{id}")]
    [Authorize(Policy = "product.update")]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _product.GetWithRelationsForWeb(id);
        if (product == null)
            return NotFound();

        // categories
        ViewBag.categories = await _category.GetAllForWeb();
        // tags
        ViewBag.tags = await _tag.GetAllForWeb();
        // attributes
        ViewBag.attributes = await _attribute.GetAllForWeb();
        // values based on choosen attruibute 
        // warehouses
        ViewBag.warehouses = await _warehouse.GetAllForWeb();
        var variant = product.ProductVariants.FirstOrDefault();
        var ava = variant?.VariantAttributeValues.FirstOrDefault();
        ViewBag.values = new List<AttributeValue>();
        if (variant != null && ava != null)
        {
            ViewBag.values = await _attribute.GetValuesForWeb(ava.AttributeId);
        }
        // Map Product entity -> ProductViewModel
        var model = _product.MapModel(product);

        return View(model);
    }

    // POST: dashboard/product/5
    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "product.update")]
    public async Task<IActionResult> Edit(ProductViewModel model, int id)
    {
        ViewBag.categories = await _category.GetAllForWeb();
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var product = await _product.UpdateForWeb(model, id);
        if (product == null)
            return NotFound();

        TempData["SuccessMessage"] = "Product updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    // POST: dashboard/product/delete/5
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "product.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        bool record = await _product.DeleteForWeb(id);
        if (!record)
            return NotFound();

        TempData["SuccessMessage"] = "Product deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
