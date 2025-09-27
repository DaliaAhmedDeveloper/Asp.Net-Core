namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/productVariant")]
public class ProductVariantController : Controller
{
    private readonly IProductVariantService _productVariant;
    private readonly IProductService _product;
    private readonly IWarehouseService _warehouse;
    private readonly IProductAttributeService _attribute;
    public ProductVariantController(
        IProductVariantService productVariant,
        IWarehouseService warehouse,
        IProductAttributeService attribute,
        IProductService product
        )
    {
        _product = product;
        _productVariant = productVariant;
        _warehouse = warehouse;
        _attribute = attribute;
    }

    // GET: dashboard/productVariant
    [HttpGet("{productId}/variants")]
    [Authorize(Policy = "variant.list")]
    public async Task<IActionResult> Index(int productId)
    {
        var product = await _product.GetForWeb(productId);
        if (product == null || product.IsDeleted == true)
            return NotFound();

        ViewBag.product = product;
        var result = await _productVariant.GetAllByProductForWeb(productId);
        return View(result);
    }

    // GET: dashboard/productVariant/5
    [HttpGet("{id}")]
    [Authorize(Policy = "variant.show")]
    public async Task<IActionResult> Details(int id)
    {
        var productVariant = await _productVariant.GetWithVavForWeb(id);
        if (productVariant == null)
            return NotFound();

        // product variant details 
        var result = await _productVariant.Mapping(productVariant);
        ViewBag.values = result.values;
        ViewBag.attributes = await _attribute.GetAllForWeb();
        ViewBag.warehouses = await _warehouse.GetAllForWeb();
        return View(result.model);
    }

    // GET: dashboard/productVariant/create
    [HttpGet("create")]
    [Authorize(Policy = "variant.add")]
    public async Task<IActionResult> Create()
    {
        ViewBag.warehouses = await _warehouse.GetAllForWeb();
        ViewBag.attributes = await _attribute.GetAllForWeb();
        return View();
    }

    // POST: dashboard/productVariant/create
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "variant.add")]
    public async Task<IActionResult> Create(ProductVariantViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.warehouses = await _warehouse.GetAllForWeb();
            ViewBag.attributes = await _attribute.GetAllForWeb();
            return View(model);
        }
        await _productVariant.CreateForWeb(model);

        TempData["SuccessMessage"] = "productVariant added successfully!";
        return RedirectToAction(nameof(Index));
    }

    // GET: dashboard/productVariant/edit/5
    [HttpGet("edit/{id}")]
    [Authorize(Policy = "variant.update")]
    public async Task<IActionResult> Edit(int id)
    {
        var productVariant = await _productVariant.GetWithVavForWeb(id);
        if (productVariant == null)
            return NotFound();

        var result = await _productVariant.Mapping(productVariant);
        ViewBag.values = result.values;
        ViewBag.warehouses = await _warehouse.GetAllForWeb();
        ViewBag.attributes = await _attribute.GetAllForWeb();
        return View(result.model);
    }

    // POST: dashboard/productVariant/edit/5
    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "variant.update")]
    public async Task<IActionResult> Edit(ProductVariantViewModel model, int id)
    {
        var productVariant = await _productVariant.GetForWeb(id);
        if (!ModelState.IsValid)
        {
            ViewBag.warehouses = await _warehouse.GetAllForWeb();
            ViewBag.attributes = await _attribute.GetAllForWeb();
            return View(model);
        }
        if (productVariant == null)
            return NotFound();

        await _productVariant.UpdateForWeb(model, productVariant);
        TempData["SuccessMessage"] = "productVariant updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    // POST: dashboard/productVariant/delete/5
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "variant.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var variant = await _productVariant.GetForWeb(id);
        var pId = variant?.ProductId;
        bool record = await _productVariant.DeleteForWeb(id);
        if (!record)
            return NotFound();

        TempData["SuccessMessage"] = "productVariant deleted successfully!";
        return RedirectToAction(nameof(Index), new { productId = pId  });

    }
}
