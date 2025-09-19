namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/coupon")]
public class CouponController : Controller
{
    private readonly ICouponService _coupon;
    public CouponController(ICouponService coupon)
    {
        _coupon = coupon;
    }

    // GET: dashboard/coupon
    [HttpGet]
    [Authorize(Policy = "coupon.list")]
    public async Task<IActionResult> Index(int? page, string searchTxt)
    {
        int pageNumber = page ?? 1;
        var results = await _coupon.GetAllWithPaginationForWeb(searchTxt, pageNumber, 10);
        return View(results);
    }

    // GET: dashboard/coupon/5
    [HttpGet("{id}")]
    [Authorize(Policy = "coupon.show")]
    public async Task<IActionResult> Details(int id)
    {
        var coupon = await _coupon.GetForWeb(id);
        if (coupon == null)
            return NotFound();

        var model = new CouponViewModel
        {
            Id = coupon.Id,

        };
        return View(model);
    }

    // GET: dashboard/coupon/create
    [HttpGet("create")]
    [Authorize(Policy = "coupon.add")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: dashboard/coupon
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "coupon.add")]
    public async Task<IActionResult> Create(CouponViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _coupon.CreateForWeb(model);
        TempData["SuccessMessage"] = "Coupon created successfully!";
        return RedirectToAction(nameof(Index));
    }

    // GET: dashboard/coupon/edit/5
    [HttpGet("edit/{id}")]
    [Authorize(Policy = "coupon.update")]
    public async Task<IActionResult> Edit(int id)
    {
        var coupon = await _coupon.GetForWeb(id);
        if (coupon == null)
            return NotFound();

        var model = new CouponViewModel
        {
            Id = coupon.Id,

        };
        return View(model);
    }

    // POST: dashboard/coupon/edit/5
    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "coupon.update")]
    public async Task<IActionResult> Edit(CouponViewModel model, int id)
    {
        var coupon = await _coupon.GetForWeb(id);
        if (!ModelState.IsValid)
            return View(model);

        if (coupon == null)
            return NotFound();

        await _coupon.UpdateForWeb(model, coupon);
        TempData["SuccessMessage"] = "Coupon updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    // POST: dashboard/coupon/delete/5
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "coupon.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        bool record = await _coupon.DeleteForWeb(id);
        if (!record)
            return NotFound();

        TempData["SuccessMessage"] = "Coupon deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
