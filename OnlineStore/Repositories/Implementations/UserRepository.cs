namespace OnlineStore.Repositories;

using OnlineStore.Models;
using OnlineStore.Models.Enums;
using OnlineStore.Helpers;
using System.Threading.Tasks;
using OnlineStore.Services;
using Microsoft.EntityFrameworkCore;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly ILanguageService _languageService;

    public UserRepository(AppDbContext context, ILanguageService languageService) : base(context)
    {
        _languageService = languageService;
    }

    // get user by email
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
    // check user
    public async Task<User?> CheckUserAsync(string email, UserType userType)
    {
        return await _context.Users.Include(u => u.Roles).ThenInclude(ur => ur.Permissions)
             .FirstOrDefaultAsync(u => u.Email == email && u.UserType == userType);

    }
    // change password
    public async Task<bool> ChangePasswordAsync(User user)
    {
        _context.Users.Attach(user); // Attach without loading from DB , tell EF there is a record in the database for this obj
        _context.Entry(user).Property(u => u.PasswordHash).IsModified = true; // tell EF only update the password field
        return await _context.SaveChangesAsync() > 0;
    }

    // web
    // get all with pagination 
    public async Task<IEnumerable<User>> GetAllWithPaginationAsync(
        string searchTxt,
        UserType userType,
        int pageNumber = 1,
        int pageSize = 10
        )
    {
        if (!string.IsNullOrEmpty(searchTxt))
            return await _context.Users.Where(u => u.UserType == userType).Where(u => u.FullName.Contains(searchTxt) || u.Email.Contains(searchTxt)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await _context.Users.Where(u => u.UserType == userType).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }
    // get with relations
    public async Task<User?> GetByIdWithRelationsAsync(int id)
    {
        return await _context.Users
            .Include(u => u.City)
            .Include(u => u.State)
            .Include(u => u.Country)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    ////
    // count all 
    public override async Task<int> CountAllAsync()
    {
        return await _context.Users.Where(u => u.UserType == UserType.User).CountAsync();
    }
    // count current month
    public override async Task<int> CountCurrentMonthAsync()
    {
        var currentMonth = DateTime.Now.Month;
        var currentYear = DateTime.Now.Year;
        return await _context.Users.Where(u => u.UserType == UserType.User)
        .Where(u => u.CreatedAt.Month == currentMonth
        && u.CreatedAt.Year == currentYear).CountAsync();
    }
    //Latest
    public override async Task<IEnumerable<User>> GetLatestAsync()
    {
        return await _context.Users.Where(u => u.UserType == UserType.User).OrderByDescending(u => u.CreatedAt).Take(4).ToListAsync();
    }

    // admins 
    public async Task<IEnumerable<User>> GetAdminsAsync()
    {
        return await _context.Users.Where(u => u.UserType == UserType.Admin).ToListAsync();  
    }

}