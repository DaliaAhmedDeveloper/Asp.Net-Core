using Microsoft.AspNetCore.Mvc;
using OnlineStore.Helpers;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Services;

namespace OnlineStore.Areas.Api.Controllers;

[Area("Api")]
[Route("[area]/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _category;
    public CategoryController(ICategoryService category)
    {
        _category = category;
    }

    // List all categories based on language 
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var categories = await _category.List();
        return Ok(ApiResponseHelper<CategoryDto>.CollectionSuccess(CategoryDto.FromModel(categories), ""));
    }
    // List all categories based on language 
    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var category = await _category.Find(id);
        return Ok(ApiResponseHelper<CategoryDetailsDto>.Success(category, ""));
    }
     // getimage
    [HttpGet("image/{imageName}")]
    public IActionResult GetImage(string imageName)
    {

        var path = Path.Combine(Directory.GetCurrentDirectory(), $"Uploads/Categories/{imageName}");
        var bytes = System.IO.File.ReadAllBytes(path);
        return File(bytes, "image/png");
    }
}