namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]

[Route("dashboard/support-ticket")]
public class SupportTicketController : Controller
{
    private readonly ISupportTicketService _supportTicket;
    public SupportTicketController(ISupportTicketService supportTicket)
    {
        _supportTicket = supportTicket;
    }

    // GET: dashboard/support-ticket
    [HttpGet]
    [Authorize(Policy = "supportTicket.list")]
    public async Task<IActionResult> Index(int? page, string searchTxt)
    {
        int pageNumber = page ?? 1;
        var results = await _supportTicket.GetAllWithPaginationForWeb(searchTxt, pageNumber, 10);
        return View(results);
    }

    // GET: dashboard/support-ticket/5
    [HttpGet("{id}")]
    [Authorize(Policy = "supportTicket.show")]
    public async Task<IActionResult> Details(int id)
    {
        var ticket = await _supportTicket.GetForWeb(id);
        if (ticket == null)
            return NotFound();

        var model = new SupportTicketViewModel
        {
            Id = ticket.Id,
            TicketNumber = ticket.TicketNumber,
            UserName = ticket.User?.FullName ?? "",
            OrderNumber = ticket.Order?.ReferenceNumber ?? "",
            Priority = ticket.Priority,
            Status = ticket.Status,
            Category = ticket.Category,
            Subject = ticket.Subject,
            Description = ticket.Description,
            // AssignedUserName = ticket.AssignedUserName,
            // AssignedUserEmail = ticket.AssignedUserEmail,
            Resolution = ticket.Resolution,
            AssignedToUserId = ticket.AssignedToUserId
        };
        return View(model);
    }

    // GET: dashboard/support-ticket/edit/5
    [HttpGet("edit/{id}")]
    [Authorize(Policy = "supportTicket.update")]
    public async Task<IActionResult> Edit(int id)
    {
        var ticket = await _supportTicket.GetForWeb(id);
        if (ticket == null)
            return NotFound();

        var model = new SupportTicketViewModel
        {
            // Id = ticket.Id,

        };
        return View(model);
    }

    // POST: dashboard/support-ticket/5
    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "supportTicket.update")]
    public async Task<IActionResult> Edit(SupportTicketViewModel model, int id)
    {
        var ticket = await _supportTicket.GetForWeb(id);
        if (!ModelState.IsValid)
            return View(model);

        if (ticket == null)
            return NotFound();

        await _supportTicket.UpdateForWeb(model, ticket);
        TempData["SuccessMessage"] = "Support ticket updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    // POST: dashboard/support-ticket/delete/5
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "supportTicket.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        bool record = await _supportTicket.DeleteForWeb(id);
        if (!record)
            return NotFound();

        TempData["SuccessMessage"] = "Support ticket deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
