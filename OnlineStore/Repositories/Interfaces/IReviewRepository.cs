namespace OnlineStore.Repositories;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;

public interface IReviewRepository : IGenericRepository<Review>
{
    Task<ReviewAttachment> AddAttachement(ReviewAttachment attachment);
    Task<IEnumerable<ReviewDto>> GetAllByUserAsync(int userId);
    Task<IEnumerable<ReviewDto>> GetAllByProductAsync(int productId);
    Task<IEnumerable<ReviewDto>> GetAllByOrderAsync(int orderId);
    Task<IEnumerable<ReviewDto>> GetAllWithPaginationAsync(string searchTxt, int page, int pageSize);
    Task<ReviewDto?> GetByIdProjectionAsync(int id);
}