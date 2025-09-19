namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using System.Threading.Tasks;

public interface ICartService
{
    Task<Cart?> FindByUserId(int userId);
    Task<CartDto?> FindDetailsByUserId(int userId);
    Task<Cart> Add(Cart cart);
    Task<bool> Update(Cart cart);
    Task<bool> Remove(int cartId);
    Task<CartDto> CheckUserCart(int userId);
}
