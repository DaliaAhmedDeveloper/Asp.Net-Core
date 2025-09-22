namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Helpers;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/Return")]
public class ReturnController : Controller
{
    private readonly IReturnService _Return;
    public ReturnController(IReturnService Return)
    {
        _Return = Return;
    }

    // GET: dashboard/Return
    [HttpGet]
    [Authorize(Policy = "return.list")]
    public async Task<IActionResult> Index(int? page, string searchTxt)
    {
        int pageNumber = page ?? 1;
        var results = await _Return.GetAllWithPaginationForWeb(searchTxt, pageNumber, 10);

        return View(results);
    }

    // GET: dashboard/Return/5
    [HttpGet("{id}")]
    [Authorize(Policy = "return.show")]
    public async Task<IActionResult> Details(int id)
    {
        var Return = await _Return.GetForWeb(id);
        if (Return == null)
            return NotFound();

        // Map to view model
        var model = new ReturnViewModel
        {
            ReferenceNumber = Return.ReferenceNumber,
            UserName = Return.Order.UserName,
            ReturnItems = Return.ReturnItems,
        };

        return View(model);
    }

    // GET: dashboard/Return/edit/5
    [HttpGet("edit/{id}")]
    [Authorize(Policy = "return.update")]
    public async Task<IActionResult> Edit(int id)
    {
        var Return = await _Return.GetForWeb(id);
        if (Return == null)
            return NotFound();

        var model = new ReturnViewModel
        {
            ReferenceNumber = Return.ReferenceNumber,
            ReturnStatus = Return.Status,
        };
        return View(model);
    }

    // POST: dashboard/Return/5
    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "return.update")]
    public async Task<IActionResult> Edit(ReturnViewModel model, int id)
    {
        var Return = await _Return.GetForWeb(id);
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        if (Return == null)
            return NotFound();

        // await _Return.UpdateStatus(Return, model.ReturnStatus);
        TempData["SuccessMessage"] = "Return updated successfully!";
        return View(model);
    }

    // POST: dashboard/Return/delete/5
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "return.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        bool record = await _Return.DeleteForWeb(id);
        if (!record)
            return NotFound();

        TempData["SuccessMessage"] = "Return deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
