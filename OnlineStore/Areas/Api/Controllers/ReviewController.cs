namespace OnlineStore.Areas.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Services;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Helpers;
using ResponseDto = OnlineStore.Models.Dtos.Responses;
using Microsoft.Extensions.Localization;

[Area("Api")]
[Authorize(AuthenticationSchemes = "Api")]
[Route("[area]/reviews")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _Review;
     private readonly IStringLocalizer<ReviewController> _localizer;
    public ReviewController(IReviewService Review, IStringLocalizer<ReviewController> localizer)
    {
        _Review = Review;
        _localizer = localizer;
    }
    // get all by order
    [HttpGet("byOrder/{orderId}")]
    public async Task<IActionResult> List(int orderId)
    {
        var Review = await _Review.ListByOrder(orderId);
        return Ok(ApiResponseHelper<IEnumerable<ResponseDto.ReviewDto>>.Success(Review, ""));
    }
    // add new Review
    [HttpPost]
    public async Task<IActionResult> Add(ReviewDto ReviewDto)
    {
        var UserId = AuthHelper.GetAuthenticatedUserId(HttpContext);
        var Review = await _Review.Add(ReviewDto, UserId);
        return Ok(ApiResponseHelper<string>.Success("", _localizer["ReviewAddedSuccessfully"]));
    }
    // delete Review
    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _Review.Delete(id);
        return Ok(ApiResponseHelper<string>.Success("", _localizer["ReviewDeletedSuccessfully"]));
    }

    // add attachement
    [HttpPost("{id}/attachement")]
    public async Task<IActionResult> AddAttachement(int id , IFormFile file)
    {
        await _Review.AddAttachement(file , id);
        return Ok(ApiResponseHelper<string>.Success("", _localizer["ReviewAttachementAddedSuccessfully"]));
    }
}