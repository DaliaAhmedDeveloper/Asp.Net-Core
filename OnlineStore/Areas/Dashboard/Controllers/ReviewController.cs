namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/Review")]
public class ReviewController : Controller
{
    private readonly IReviewService _review;
     private readonly ICountryService _country;
    public ReviewController(IReviewService review, ICountryService country)
    {
        _review = review;
        _country = country;
    }

    // GET: dashboard/Review
    [HttpGet]
    [Authorize(Policy = "review.list")]
    public async Task<IActionResult> Index(int? page, string searchTxt)
    {
        int pageNumber = page ?? 1;
        var results = await _review.GetAllWithPaginationForWeb(searchTxt, pageNumber, 10);
        return View(results);
    }

    // GET: dashboard/Review/5
    [HttpGet("{id}")]
    [Authorize(Policy = "review.show")]
    public async Task<IActionResult> Details(int id)
    {
        var review = await _review.GetForWeb(id);
        return View(review);
    }
    // accept
    
    // POST: dashboard/Review/delete/5
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "review.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        bool record = await _review.DeleteForWeb(id);
        if (!record)
            return NotFound();

        TempData["SuccessMessage"] = "Review deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
