namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;
using ResponseDto = OnlineStore.Models.Dtos.Responses;

using OnlineStore.Models.ViewModels;

public interface IReviewService
{
    Task<IEnumerable<Review>> List();
    Task<IEnumerable<ResponseDto.ReviewDto>> ListByUser(int userId);
    Task<IEnumerable<ResponseDto.ReviewDto>> ListByOrder(int userId);
    Task<IEnumerable<ResponseDto.ReviewDto>> ListByProduct(int ProductId);
    Task<Review> Add(ReviewDto review, int userId);
    Task<bool> AddAttachement(IFormFile file, int reviewId);
    Task<bool> Delete(int id);
    // web
    Task<PagedResult<ResponseDto.ReviewDto>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber, int pageSize);
    Task<IEnumerable<Review>> GetAllForWeb();
    Task<bool> DeleteForWeb(int id);
    Task<ResponseDto.ReviewDto?> GetForWeb(int id);
}