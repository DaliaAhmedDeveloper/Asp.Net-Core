namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.Dtos.Requests;

using OnlineStore.Models.Enums;
using OnlineStore.Models.ViewModels;

public interface IUserService
{
   Task<User?> CheckUser(string Email, string Password, UserType UserType);
   Task<UserDto> SignIn(LoginDto model);
   Task<UserDto> ExternalLoginCallback(HttpContext httpContext);
   Task<bool> LogOut(HttpContext httpContext);
   Task<IEnumerable<User>> GetAll();
   Task<User> Find(int id);
   Task<User?> FindByEmail(string email);
   Task<UserDto> Add(CreateUserDto model);
   Task<User?> UpdateProfile(int id, UpdateUserDto userDto);
   Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto);
   Task Delete(int id);
   //Task<Wallet?> Wallet(int userId);

   // web
   Task<PagedResult<User>> GetAllWithPaginationForWeb(string searchTxt, UserType userType, int pageNumber, int pageSize);
   Task<User> CreateForWeb(UserViewModel model);
   Task<User?> GetForWeb(int id);
   Task<IEnumerable<User>> GetAllForWeb();
   Task<User> UpdateForWeb(UpdateUserViewModel model, User User);
   Task<bool> DeleteForWeb(int id);
   Task<IEnumerable<User>> GetAdmins();
}