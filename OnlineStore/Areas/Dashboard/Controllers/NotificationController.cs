namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Helpers;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/notification")]
public class NotificationController : Controller
{
    private readonly INotificationService _notification;
    public NotificationController(INotificationService notification)
    {
        _notification = notification;
    }

    // GET: dashboard/notification
    [HttpGet]
    [Authorize(Policy = "notification.list")]
    public async Task<IActionResult> Index(int? page, string searchTxt)
    {
        int pageNumber = page ?? 1;
        int userId = AuthHelper.GetAuthenticatedUserId(HttpContext);
        var results = await _notification.GetAllWithPaginationForWeb(userId ,searchTxt, pageNumber, 10);
        return View(results);
    }
    // POST: dashboard/notification/delete/5
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "notification.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        bool record = await _notification.DeleteForWeb(id);
        if (!record)
            return NotFound();

        TempData["SuccessMessage"] = "Notification deleted successfully!";
        return Content("success ");
    }
}
