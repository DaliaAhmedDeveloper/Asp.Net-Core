namespace OnlineStore.Repositories;

using OnlineStore.Models;
public interface ICartItemRepository : IGenericRepository<CartItem>
{
    Task<bool> DeleteAllAsync(int cartItemId);
    Task<CartItem?> GetByProductIdAndVariantIdAsync(int cartId , int productId, int? VariantId);
    Task<IEnumerable<CartItem>> GetAllByCartAsync(int carId);
}