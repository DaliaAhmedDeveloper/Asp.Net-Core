namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/warehouse")]
public class WarehouseController : Controller
{
    private readonly IWarehouseService _warehouse;
    public WarehouseController(IWarehouseService warehouse)
    {
        _warehouse = warehouse;
    }

    // GET: Dashboard/warehouse
    [HttpGet]
    [Authorize(Policy = "warehouse.list")]
    public async Task<IActionResult> Index(int? page, string searchTxt)
    {
        int pageNumber = page ?? 1;
        var results = await _warehouse.GetAllWithPaginationForWeb(searchTxt, pageNumber, 10);
        return View(results);
    }

    // GET: Dashboard/warehouse/Details/5
    [HttpGet("{id}")]
    [Authorize(Policy = "warehouse.show")]
    public async Task<IActionResult> Details(int id)
    {
        var model = await _warehouse.GetForWeb(id);
        if (model == null)
            return NotFound();

        var warehouse = new WarehouseViewModel
        {
            Id = model.Id,
            Code = model.Code,
            Name = model.Name,
            Address = model.Address,
            City = model.City,
            State = model.State,
            Country = model.Country,
            ZipCode = model.ZipCode,
            Phone = model.Phone,
            Email = model.Email,
            IsActive = model.IsActive,
            IsDefault = model.IsDefault,
        };

        return View(warehouse);
    }

    // GET: Dashboard/warehouse/Create
    [HttpGet("create")]
    [Authorize(Policy = "warehouse.add")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Dashboard/warehouse/Create
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "warehouse.add")]
    public async Task<IActionResult> Create(WarehouseViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _warehouse.CreateForWeb(model);
        TempData["SuccessMessage"] = "Warehouse Created successfully!";
        return RedirectToAction(nameof(Index));
    }
    // GET: Dashboard/warehouse/Edit/5
    [HttpGet("edit/{id}")]
    [Authorize(Policy = "warehouse.update")]
    public async Task<IActionResult> Edit(int id)
    {
        var model = await _warehouse.GetForWeb(id);
        if (model == null)
            return NotFound();

        var warehouse = new WarehouseViewModel
        {
            Id = model.Id,
            Code = model.Code,
            Name = model.Name,
            Address = model.Address,
            City = model.City,
            State = model.State,
            Country = model.Country,
            ZipCode = model.ZipCode,
            Phone = model.Phone,
            Email = model.Email,
            IsActive = model.IsActive,
            IsDefault = model.IsDefault,
        };

        return View(warehouse);
    }

    // POST: Dashboard/warehouse/Edit/5
    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "warehouse.update")]
    public async Task<IActionResult> Edit(WarehouseViewModel model, int id)
    {
        if (!ModelState.IsValid)
            return View(model);

        var warehouse = await _warehouse.GetForWeb(id);
        if (warehouse == null)
            return NotFound();

        await _warehouse.UpdateForWeb(model, warehouse);
        TempData["SuccessMessage"] = "Warehouse updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    // POST: Dashboard/warehouse/Delete/5
    [HttpPost("delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "warehouse.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var warehouse = await _warehouse.GetForWeb(id);
        if (warehouse == null)
            return NotFound();

        await _warehouse.DeleteForWeb(id);
        TempData["SuccessMessage"] = "Warehouse Deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}