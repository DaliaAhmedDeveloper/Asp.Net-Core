namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/appSetting")]
public class AppSettingController : Controller
{
    private readonly IAppSettingService _appSetting;
    public AppSettingController(IAppSettingService appSetting)
    {
        _appSetting = appSetting;
    }

    // GET: dashboard/AppSetting
    [HttpGet]
    [Authorize(Policy = "settings.list")]
    public async Task<IActionResult> Edit()
    {
        var results = await _appSetting.List();
        return View(results);
    }

    [HttpPost]
    [Authorize(Policy = "settings.update")]
    public async Task<IActionResult> Edit(Dictionary<int, string> settings)
    {
        await _appSetting.Update(settings);
        TempData["SuccessMessage"] = "Settings Updated added successfully";
        var entities = await _appSetting.List();
        return View(entities);
    }
}