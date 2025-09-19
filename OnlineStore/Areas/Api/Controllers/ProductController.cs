namespace OnlineStore.Areas.Api.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OnlineStore.Helpers;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Services;

[ApiController]
[Route("[area]/products")]
[Area("Api")]
public class ProductController : ControllerBase
{
    private readonly IProductService _product;
    private readonly IReviewService _review;
    private readonly IStringLocalizer<ProductController> _localizer;

    public ProductController(IProductService product, IReviewService review, IStringLocalizer<ProductController> localizer)
    {
        _product = product;
        _review = review;
        _localizer = localizer;
    }

    // api/products  -- get
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _product.ListBasedOnLang();
        var response = ApiResponseHelper<ProductAllDto>.CollectionSuccess(ProductAllDto.FromModel(products), "");
        return Ok(response);
    }

    // api/products/id  -- get
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _product.Find(id);
        if (product == null)
            throw new NotFoundException(_localizer["ProductNotFound"]);

        return Ok(ApiResponseHelper<ProductDto>.Success(product, ""));
    }

    // product search
    // by title or sku
    [HttpGet("search/{searchText}")]
    public async Task<IActionResult> Serach(string searchText)
    {
        var results = await _product.ProductSearch(searchText);
        return Ok(ApiResponseHelper<ProductAllDto>.CollectionSuccess(ProductAllDto.FromModel(results), ""));
    }

    // product filter
    // filter by category , filter by tags , filter by attribute value , filter by price
    [HttpPost("filter")]
    public async Task<IActionResult> Filter(ProductFilterDto productFilterDto)
    {
        var results = await _product.ProductFilter(productFilterDto);
        return Ok(ApiResponseHelper<ProductAllDto>.CollectionSuccess(ProductAllDto.FromModel(results), ""));
    }

    // // product reviews 
    // [HttpGet("{id}/reviews")]
    // public async Task<IActionResult> Reviews(int id)
    // {
    //     var reviews = await _review.ListByProduct(id);
    //     return Ok(ApiResponseHelper<Review>.CollectionSuccess(reviews, ""));
    // }
}