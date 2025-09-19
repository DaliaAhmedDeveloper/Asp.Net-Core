namespace OnlineStore.Repositories;


using System.Security.Claims;
using OnlineStore.Models;
using OnlineStore.Models.Enums;

public interface IUserRepository : IGenericRepository<User>
{
   Task<User?> GetByEmailAsync(string email);
   Task<User?> CheckUserAsync(string email, UserType userType);
   Task<bool> ChangePasswordAsync(User user);

   //web
   Task<IEnumerable<User>> GetAllWithPaginationAsync(string searchTxt, UserType userType, int page, int pageSize);
   Task<User?> GetByIdWithRelationsAsync(int id);
   Task<IEnumerable<User>> GetAdminsAsync();
}