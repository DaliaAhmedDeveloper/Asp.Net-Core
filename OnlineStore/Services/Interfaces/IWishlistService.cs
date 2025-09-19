namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;

public interface IWishlistService
{
    Task <Wishlist?> Add(int userId , int productId);
    Task<bool> Delete(int id);
    Task<IEnumerable<WishlistDto>> ListByUser(int userId);
}