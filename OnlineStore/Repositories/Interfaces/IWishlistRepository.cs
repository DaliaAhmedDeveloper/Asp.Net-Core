namespace OnlineStore.Repositories;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;

public interface IWishlistRepository
{
    Task<Wishlist> AddAsync(Wishlist whishlist);
    Task<bool> DeleteAsync(int whishlistId);
    Task<IEnumerable<WishlistDto>> GetAllByUserAsync(int userId);
    Task<Wishlist?> GetByUserAndProduct(int userId, int productId);
}