namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Enums;
using OnlineStore.Helpers;
using System.Threading.Tasks;
using OnlineStore.Repositories;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Models.Dtos.Responses;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using OnlineStore.Models.ViewModels;
using System.Security.Cryptography;
using Microsoft.Extensions.Localization;
using OnlineStore.Services.BackgroundServices;
using OnlineStore.Notifications;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;
    private readonly IRefreshTokenRepository _refreshTokenRepo;
    private readonly AppSettings _appSettings;
    private readonly IStringLocalizer<UserService> _localizer;
    private readonly IBackgroundTaskQueue _taskQueue;
    private readonly IServiceScopeFactory _scopeFactory;
    public UserService(
        IRefreshTokenRepository refreshTokenRepo,
        IUserRepository userRepo,
        IOptions<AppSettings> settings,
        IStringLocalizer<UserService> localizer,
        IBackgroundTaskQueue taskQueue,
        IServiceScopeFactory scopeFactory
    )
    {
        _appSettings = settings.Value;
        _userRepo = userRepo;
        _refreshTokenRepo = refreshTokenRepo;
        _localizer = localizer;
        _taskQueue = taskQueue;
        _scopeFactory = scopeFactory;
    }
    // check user email and password for authentication
    public async Task<User?> CheckUser(string Email, string Password, UserType UserType)
    {
        // check user by email and user type
        var user = await _userRepo.CheckUserAsync(Email, UserType);
        if (user != null)
        {
            // check if the password matches
            if (!PasswordHelper.VerifyPassword(user.PasswordHash, Password))
            {
                user = null;
            }
        }
        return user;
    }

    // sign in
    public async Task<UserDto> SignIn(LoginDto login)
    {
        // Validate user credentials
        if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
            throw new ArgumentException(_localizer["EmailAndPasswordRequired"]);

        User? user = await CheckUser(login.Email, login.Password, UserType.User);
        if (user == null)
            throw new NotFoundException(_localizer["UserNotFound"]);

        var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), new Claim(ClaimTypes.Email, login.Email) };
        return await GenerateTokens(claims, user);
    }
    // extenel Login for social media
    public async Task<UserDto> ExternalLoginCallback(HttpContext httpContext)
    {
        var result = await httpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (result == null || result.Principal == null || result.Properties == null)
            throw new BadHttpRequestException(_localizer["ExternalLoginFailed"]);

        var identity = result.Principal.Identities.FirstOrDefault();
        if (identity == null)
            throw new BadHttpRequestException(_localizer["ExternalLoginFailed"]);

        var claims = identity.Claims;

        var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? "";

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name))
            if (string.IsNullOrEmpty(email))
                throw new BadHttpRequestException(_localizer["ExternalLoginFailed"]);

        // Check if user exists in the database
        var user = await _userRepo.CheckUserAsync(email, UserType.User);

        // check the provider
        ProviderType provider;
        switch (result.Properties.Items[".AuthScheme"])
        {
            case "Google":
                provider = ProviderType.Google;
                break;
            case "Facebook":
                provider = ProviderType.Facebook;
                break;
            default:
                throw new Exception(_localizer["UnsupportedProvider"]);
        }
        if (user == null)
        {
            // Signup: Create new user
            user = new User
            {
                FullName = name,
                Email = email,
                Provider = provider,
            };
            await _userRepo.AddAsync(user);
        }
        // Login: Generate JWT or start session
        return await GenerateTokens(claims.ToList(), user);
    }

    // log out
    public async Task<bool> LogOut(HttpContext httpContext)
    {
        // Get the authenticated user's claims principal
        var user = httpContext.User;
        if (user?.Identity == null || !user.Identity.IsAuthenticated)
            throw new NotFoundException(_localizer["UserNotFound"]);

        // Get specific claims, e.g. user id or email
        var email = user.FindFirstValue(ClaimTypes.Email);
        if (email == null)
            throw new NotFoundException(_localizer["EmailNotFound"]);

        User? userDb = await FindByEmail(email);
        if (userDb == null)
            throw new NotFoundException(_localizer["UserNotFound"]);

        var userRefreshToken = await _refreshTokenRepo.GetByUserIdAsync(userDb.Id);
        if (userRefreshToken == null)
            throw new NotFoundException(_localizer["RefreshTokenNotFound"]);

        bool status = await _refreshTokenRepo.DeleteAsync(userRefreshToken.Id);
        if (!status)
            throw new NotFoundException(_localizer["RefreshTokenNotFound"]);
        return true;
    }
    //get all users 
    public async Task<IEnumerable<User>> GetAll()
    {
        return await _userRepo.GetAllAsync();
    }
    //get by email
    public async Task<User?> FindByEmail(string email)
    {
        return await _userRepo.GetByEmailAsync(email);
    }

    // get user info
    public async Task<User> Find(int id)
    {
        var user = await _userRepo.GetByIdAsync(id);
        if (user == null)
            throw new NotFoundException(string.Format(_localizer["UserWithIdNotFound"], id));
        return user;
    }
    // add new user (use DTO here )
    public async Task<UserDto> Add(CreateUserDto model)
    {
        if (model.CityId == null || model.CountryId == null || model.StateId == null)
            throw new ResponseErrorException(_localizer["CountryStateCityRequired"]);

        var user = new User
        {
            FullName = model.FullName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            PasswordHash = PasswordHelper.HashPassword(model.Password), // You should hash the password!
            CountryId = model.CountryId.Value,
            CityId = model.CityId.Value,
            StateId = model.StateId.Value
        };
        var userAdded = await _userRepo.AddAsync(user);
        
        // send notifications  + signalR
        _taskQueue.Enqueue(async token =>
        {
            using var scope = _scopeFactory.CreateAsyncScope();
            var _user = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            var _notification = scope.ServiceProvider.GetRequiredService<INotificationRepository>();
            var _push = scope.ServiceProvider.GetRequiredService<PushNotificationHelper>();

            // send notification + signalr
            var admins = await _user.GetAdminsAsync();
            foreach (var admin in admins)
            {
                var notification = NewUserAdminNotification.Build(admin.Id, userAdded);
                await _notification.AddAsync(notification);
                var notificationDto = new NotificationDto
                {
                    Type = notification.Type,
                    Url = notification.Url,
                    Title = notification.Translations.Where(tr => tr.LanguageCode == "en").Select(tr => tr.Title).FirstOrDefault() ?? "",
                    Message = notification.Translations.Where(tr => tr.LanguageCode == "en").Select(tr => tr.Message).FirstOrDefault() ?? "",
                    NotificationRelated = PushNotificationType.NewAccount.ToString()
                };
                await _push.PushToUser(admin.Id, notificationDto);
            }
        });
        //send emails
        _taskQueue.Enqueue(async token =>
        {
            using var scope = _scopeFactory.CreateAsyncScope();
            var _email = scope.ServiceProvider.GetRequiredService<IEmailService>();
            var _appSettings = scope.ServiceProvider.GetRequiredService<AppSettingHelper>();
            var _configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var templateService = new EmailTemplateService();

            //user welcome email
            var body = await templateService.RenderAsync("User/WelcomeEmail", userAdded);
            await _email.SendEmailAsync(model.Email, "Welcome Email", body);

            // send admin email 
            var baseUrl = _configuration["AppSettings:BaseUrl"];
            var userUrl = $"{baseUrl}/dashboard/user/{userAdded.Id}";
            var viewModel = new NewUserEmailViewModel
            {
                User = userAdded,
                Url = userUrl
            };
            var adminEmailBody = await templateService.RenderAsync("Admin/NewUser", viewModel);
            await _email.SendEmailAsync(await _appSettings.GetValue("admin_email"), "New Account", adminEmailBody);
        });
        // response dto
        var userDto = new UserDto
        {
            Id = userAdded.Id,
            FullName = userAdded.FullName,
            PhoneNumber = userAdded.PhoneNumber,
            Email = userAdded.Email,
            UserType = userAdded.UserType
        };
        return userDto;
    }
    // update user 
    public async Task<User?> UpdateProfile(int id, UpdateUserDto userDto) // use dto
    {
        var user = await _userRepo.GetByIdAsync(id);
        if (user == null) return null;

        user.FullName = userDto.FullName;
        bool status = await _userRepo.UpdateAsync(user);
        if (!status) return null;
        return user;
    }
    // change password 
    public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto)
    {
        string newPassword = PasswordHelper.HashPassword(changePasswordDto.Password);
        var user = new User { Id = userId, PasswordHash = newPassword }; // Only setting the primary key
        return await _userRepo.ChangePasswordAsync(user); // return boolean 
    }
    // delete user 
    public async Task Delete(int id)
    {
        bool status = await _userRepo.DeleteAsync(id);
        if (!status)
            throw new NotFoundException(string.Format(_localizer["UserWithIdNotFound"], id));
    }

    // generate Tokens
    private async Task<UserDto> GenerateTokens(List<Claim> claims, User user)
    {
        var accessToken = JwtHelper.GenerateAccessToken(claims, _appSettings.JwtSecurityKey, _appSettings.Issuer, _appSettings.Audience);
        var refreshToken = JwtHelper.GenerateRefreshToken(user.Id);

        // create or update refreshToken in DB
        var refreshTokenExists = await _refreshTokenRepo.GetByUserAsync(user.Id);
        if (refreshTokenExists == null)
        {
            await _refreshTokenRepo.AddAsync(refreshToken);
        }
        else
        {
            refreshTokenExists.ExpiryDate = DateTime.UtcNow.AddDays(20);
            refreshTokenExists.Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            await _refreshTokenRepo.UpdateAsync(refreshTokenExists);
        }

        var response = new UserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            UserType = user.UserType
        };
        response.Token = accessToken;
        response.RefreshToken = refreshToken.Token;
        return response;
    }
    // WEB
    // get
    public async Task<IEnumerable<User>> GetAllForWeb()
    {
        return await _userRepo.GetAllAsync();
    }
    // get all with pagination
    public async Task<PagedResult<User>> GetAllWithPaginationForWeb(string searchTxt, UserType userType, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _userRepo.CountAllAsync();
        var users = await _userRepo.GetAllWithPaginationAsync(searchTxt, userType, pageNumber, pageSize);
        var model = new PagedResult<User>
        {
            Items = users,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }
    // get details
    public async Task<User?> GetForWeb(int id)
    {
        return await _userRepo.GetByIdWithRelationsAsync(id);
    }

    // add new user
    public async Task<User> CreateForWeb(UserViewModel model)
    {
        var user = new User
        {
            Id = model.Id,
            FullName = model.FullName ?? "",
            Email = model.Email ?? "",
            UserType = model.UserType,
            Provider = model.Provider,
            PhoneNumber = model.PhoneNumber ?? "",
            IsActive = model.IsActive,
            CountryId = model.CountryId,
            StateId = model.StateId,
            CityId = model.CityId,
        };

        await _userRepo.AddAsync(user);
        return user;
    }
    // update user
    public async Task<User> UpdateForWeb(UpdateUserViewModel model, User user)
    {
        if (!string.IsNullOrEmpty(model.PasswordHash))
        {
            user.PasswordHash = PasswordHelper.HashPassword(model.PasswordHash);
        }
        user.FullName = model.FullName;
        // user.Email = model.Email;
        // user.PhoneNumber = model.PhoneNumber;
        user.CountryId = model.CountryId;
        user.CityId = model.CityId;
        user.StateId = model.StateId;

        await _userRepo.UpdateAsync(user);
        return user;
    }
    // delete user
    public async Task<bool> DeleteForWeb(int id)
    {
        return await _userRepo.DeleteAsync(id);
    }
    // get Admins
    public async Task<IEnumerable<User>> GetAdmins()
    {
        return await _userRepo.GetAdminsAsync();

    }
}