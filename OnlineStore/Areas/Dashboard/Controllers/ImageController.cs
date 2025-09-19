namespace OnlineStore.Areas.Dashboard.Controllers;
using Microsoft.AspNetCore.Mvc;

[Area("Dashboard")]
public class ImageController : Controller
{
    // getimage
    [HttpGet("/category/image/{imageName}")]
    public IActionResult GetCategoryImage(string imageName)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), $"Uploads/Categories/{imageName}");
        var bytes = System.IO.File.ReadAllBytes(path);
        return File(bytes, "image/png");
    }

     // getimage
    [HttpGet("/Product/image/{imageName}")]
    public IActionResult GetProductImage(string imageName)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), $"Uploads/Products/{imageName}");
        var bytes = System.IO.File.ReadAllBytes(path);
        return File(bytes, "image/png");
    }
}
