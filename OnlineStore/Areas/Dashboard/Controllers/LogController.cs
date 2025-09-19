namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/log")]
public class LogController : Controller
{
    private readonly ILogService _log;
    public LogController(ILogService log)
    {
        _log = log;
    }

    // GET: dashboard/log
    [HttpGet]
    [Authorize(Policy = "logs.list")]
    public async Task<IActionResult> Index(int? page, string searchTxt)
    {
        int pageNumber = page ?? 1;
        var results = await _log.GetAllWithPaginationForWeb(searchTxt, pageNumber, 10);
        return View(results);
    }

    // GET: dashboard/log/5
    [HttpGet("{id}")]
    [Authorize(Policy = "logs.show")]
    public async Task<IActionResult> Details(int id)
    {
        var log = await _log.GetForWeb(id);
        if (log == null)
            return NotFound();

        var model = new LogViewModel
        {
            StackTrace = log.StackTrace,
            InnerException = log.InnerException,
            ExceptionTypeEn = log.Translations.FirstOrDefault(tr => tr.LanguageCode == "en")?.ExceptionTitle ?? "",
            ExceptionMessageEn = log.Translations.FirstOrDefault(tr => tr.LanguageCode == "en")?.ExceptionMessage ?? "",
            ExceptionTypeAr = log.Translations.FirstOrDefault(tr => tr.LanguageCode == "ar")?.ExceptionTitle ?? "",
            ExceptionMessageAr = log.Translations.FirstOrDefault(tr => tr.LanguageCode == "ar")?.ExceptionMessage ?? "",
            CreatedAt = log.CreatedAt
        };

        return View(model);
    }
    // POST: dashboard/log/delete/5
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "log.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        bool record = await _log.DeleteForWeb(id);
        if (!record)
            return NotFound();

        TempData["SuccessMessage"] = "Log deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
    // delete all 
    
}
