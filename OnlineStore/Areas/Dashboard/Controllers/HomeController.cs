namespace OnlineStore.Areas.Dashboard.Controllers;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using Microsoft.AspNetCore.Authorization;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
public class HomeController : Controller
{
    private readonly IDashboardService _dashboard;
    public HomeController(IDashboardService dashboard)
    {
        _dashboard = dashboard;
    }

    //[Authorize(Policy = "Home")]
    [Route("dashboard/index")]
    public async Task<IActionResult> Index()
    {
        var model = await _dashboard.Dashboard();
        return View(model);
    }

    public IActionResult Mentainane()
    {
        return Content("Website Under Mentainane");
    }
    public IActionResult Privacy() // IActionResult : interface able to return some different of types
    {
        return View();
        // return BadRequest("خطأ");	
        // return NotFound();	
        //return Redirect("/home/index");
        // return View(); أو return View(model);	
        //return Json(data);		
        //return Ok(product) : return request status with json 
        // return Content("نص عادي");	
        // return File(fileBytes, contentType);	// return a file to download or show 
    }

    public IActionResult MyMethod()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] //??
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
