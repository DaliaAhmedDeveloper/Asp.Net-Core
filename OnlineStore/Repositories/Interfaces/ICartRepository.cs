namespace OnlineStore.Repositories;

using System.Threading.Tasks;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;

public interface ICartRepository
{
    Task<Cart?> GetByUserIdAsync(int userId);
    Task<CartDto?> GetByUserIdDetailsAsync(int userId);
    Task<Cart> AddAsync(Cart cart);
    Task<bool> UpdateAsync(Cart cart);
    Task<bool> DeleteAsync(int cartId);
}
