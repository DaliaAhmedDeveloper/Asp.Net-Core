namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Helpers;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using OnlineStore.Services.BackgroundServices;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/ticket-message")]
public class TicketMessageController : Controller
{
    private readonly ITicketMessageService _ticketMessage;
    private readonly ISupportTicketService _ticket;
    public TicketMessageController(
        ISupportTicketService ticket,
        ITicketMessageService ticketMessage
        )
    {
        _ticketMessage = ticketMessage;
        _ticket = ticket;
    }
    // GET: dashboard/ticket-message/5
    [HttpGet("{id}")]
    [Authorize(Policy = "ticketMessage.list")]
    public async Task<IActionResult> Index(int id)
    {
        var ticket = await _ticket.GetForWeb(id);
        if (ticket == null)
            return NotFound();

        var messages = await _ticketMessage.GetByTicketForWeb(id);
        ticket.Messages = (ICollection<Models.TicketMessage>)messages;

        TicketMessagesPageViewModel model = new TicketMessagesPageViewModel
        {
            Ticket = ticket,
            Message = new TicketMessageViewModel()
        };
        return View(model);
    }
    // POST: dashboard/ticket-message
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "ticketMessage.add")]
    public async Task<IActionResult> Create(TicketMessagesPageViewModel model)
    {
        int ticketId = model.Message.TicketId;

        var ticket = await _ticket.GetForWeb(ticketId);
        if (ticket == null)
            return NotFound();

        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Index), new { id = ticketId });

        // model.UserId = AuthHelper.GetAuthenticatedUserId(HttpContext);
        model.Message.UserId = 1;
        model.Message.IsFromStaff = true;
        await _ticketMessage.CreateForWeb(model.Message);
        TempData["SuccessMessage"] = "Message Sent successfully!";
        // send email and notification
        return RedirectToAction(nameof(Index), new { id = ticketId });
    }
    // POST: dashboard/ticket-message/delete/5
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "ticketMessage.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        bool record = await _ticketMessage.DeleteForWeb(id);
        if (!record)
            return NotFound();

        TempData["SuccessMessage"] = "Message deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
