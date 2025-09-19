namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;


[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/Role")]
public class RoleController : Controller
{
    private readonly IRoleService _role;
    public RoleController(IRoleService role)
    {
        _role = role;
    }

    // GET: dashboard/role
    [HttpGet]
    [Authorize(Policy = "role.list")]
    public async Task<IActionResult> Index(int? page, string searchTxt)
    {
        int pageNumber = page ?? 1;
        var results = await _role.GetAllWithPaginationForWeb(searchTxt, pageNumber, 10);
        return View(results);
    }

    // GET: dashboard/role/5
    [HttpGet("{id}")]
    [Authorize(Policy = "role.show")]
    public async Task<IActionResult> Details(int id)
    {
        var role = await _role.GetForWeb(id);
        if (role == null)
            return NotFound();

        return View(role);
    }

    // GET: dashboard/role/create
    [HttpGet("create")]
    [Authorize(Policy = "role.add")]
    public async Task<IActionResult> Create()
    {
        ViewBag.permissions = await _role.Permissions();
        return View();
    }

    // POST: dashboard/role
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "role.add")]
    public async Task<IActionResult> Create(RoleFormViewModel model)
    {
        ViewBag.permissions = await _role.Permissions();
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _role.CreateForWeb(model);
        TempData["SuccessMessage"] = "role added successfully!";
        return RedirectToAction(nameof(Index));
    }

    // GET: dashboard/role/edit/5
    [HttpGet("edit/{id}")]
    [Authorize(Policy = "role.update")]
    public async Task<IActionResult> Edit(int id)
    {
        var role = await _role.GetForWeb(id);
        if (role == null)
            return NotFound();

        var model = new RoleFormViewModel
        {
            Id = role.Id,
            NameAr = role.NameAr,
            NameEn = role.NameEn,
            DescriptionAr = role.DescriptionAr,
            DescriptionEn = role.DescriptionEn,
            Slug = role.Slug,
            SelectedPermissions = role.Permissions.Select(p => p.Id).ToList(),
            Permissions = role.Permissions
        };
        ViewBag.permissions = await _role.Permissions();
        return View(model);
    }

    // POST: dashboard/role/edit/5
    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "role.update")]
    public async Task<IActionResult> Edit(RoleFormViewModel model, int id)
    {
        var role = await _role.WithRelations(id);

        if (!ModelState.IsValid)
        {
            return View(model);
        }
        if (role == null)
            return NotFound();

        await _role.UpdateForWeb(model, role);
        ViewBag.permissions = await _role.Permissions();
        TempData["SuccessMessage"] = "Role updated successfully!";
        return View(model);
    }

    // POST: dashboard/role/delete/5
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "role.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        bool record = await _role.DeleteForWeb(id);
        if (!record || id == 1)
            return NotFound();

        TempData["SuccessMessage"] = "role deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}