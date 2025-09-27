using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Repositories;
using OnlineStore.Helpers;
using OnlineStore.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using OnlineStore.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;
using Microsoft.AspNetCore.RateLimiting;
using OnlineStore.Services.BackgroundServices;
using Microsoft.AspNetCore.Authorization;
using OnlineStore.Providers;
using Serilog;
using OnlineStore.Hubs;
/* 
where is this args parameter?? it comes from the command dotnet run pla pla pla
it returns object from WebApplicationBuilder class 
This object is responsible for building everything initially (configuration, services, logging... etc).
*/
var builder = WebApplication.CreateBuilder(args);

/*
Adds MVC services to the DI container so you can use controllers & views services
builder.Services returns IServiceCollection which is a list of services
AddControllersWithViews is an extension method on IServiceCollection.
*/
builder.Services.AddControllersWithViews()
    // add support of localization inside views
    .AddViewLocalization()
    // add support of localization for dataannotation validation messages
    .AddDataAnnotationsLocalization();


// register classess (services)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))); // sql connection

/* 
Repositories for Database (Ef)
Means when you want IProductRepository use ProductRepository class
*/
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<IWalletTransactionRepository, WalletTransactionRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IUserPointRepository, UserPointRepository>();
builder.Services.AddScoped<IDataBaseExistsRepository, DataBaseExistsRepository>();
builder.Services.AddScoped<IShippingMethodRepository, ShippingMethodRepository>();

builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IStateRepository, StateRepository>();

builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IStockMovementRepository, StockMovementRepository>();
builder.Services.AddScoped<IOrderTrackingRepository, OrderTrackingRepository>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddScoped<IProductVariantRepository, ProductVariantRepository>();

builder.Services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
builder.Services.AddScoped<IProductAttributeValueRepository, ProductAttributeValueRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();

builder.Services.AddScoped<ISupportTicketRepository, SupportTicketRepository>();
builder.Services.AddScoped<ITicketMessageRepository, TicketMessageRepository>();
builder.Services.AddScoped<IAppSettingRepository, AppSettingRepository>();
builder.Services.AddScoped<IReturnRepository, ReturnRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

//services -- for Bussiness Logic
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<IQueryService, QueryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<IWishlistService, WishlistService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<IWalletTransactionService, WalletTransactionService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserPointService, UserPointService>();
builder.Services.AddScoped<IShippingMethodService, ShippingMethodService>();

builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IStateService, StateService>();

builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IProductAttributeService, ProductAttributeService>();
builder.Services.AddScoped<IProductAttributeValueService, ProductAttributeValueService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();

builder.Services.AddScoped<ISupportTicketService, SupportTicketService>();
builder.Services.AddScoped<ITicketMessageService, TicketMessageService>();
builder.Services.AddScoped<IProductVariantService, ProductVariantService>();
builder.Services.AddScoped<IAppSettingService, AppSettingService>();
builder.Services.AddScoped<IReturnService, ReturnService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IRoleService, RoleService>();

// add signalR
builder.Services.AddSignalR();
//Register localization helper 
builder.Services.AddScoped<LocalizationHelper>(); // question : why i register this here ?
builder.Services.AddScoped<OrderHelper>();
builder.Services.AddScoped<ReturnHelper>();
builder.Services.AddScoped<AppSettingHelper>();
builder.Services.AddScoped<PushNotificationHelper>();

/*
specify the folder of localization 
*/
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    //supported languages
    var supportedCultures = new[] { "en", "ar" };
    // default language
    options.SetDefaultCulture(supportedCultures[0])
           // Specifies which formats (date, number, currency) will be supported based on selected language.
           .AddSupportedCultures(supportedCultures)
           // responsible for return translations based on selected language
           .AddSupportedUICultures(supportedCultures);
});

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Warning() // logs Warning and above
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Replace default logging
builder.Host.UseSerilog();

/*
It's a service that allows you to access HttpContext from non-controller classes (e.g., services, repositories).
*/
builder.Services.AddHttpContextAccessor();

//Add custom logging provider
builder.Services.AddSingleton<ILoggerProvider, DbLoggerProvider>();

/*
Register Background Services
*/
builder.Services.AddHostedService<CartReminderService>();
builder.Services.AddHostedService<UserPointExpiryService>();
builder.Services.AddHostedService<QueuedHostedService>();

builder.Services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();

// register Dynamic Policy Provider
builder.Services.AddSingleton<IAuthorizationPolicyProvider, DynamicAuthorizationPolicyProvider>();

/*
Register Email Service
*/
builder.Services.Configure<EmailSetting>(
    builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddTransient<IEmailService, EmailService>();
/*
change the default views directory
1 means controller name 
0 means action name 
*/
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationFormats.Add("/Views/Dashboard/{1}/{0}.cshtml");
});

/* 
json settings

Prevents infinite loops when serializing objects with circular references.
Instead of throwing an error, it skips serializing the repeated reference.
Safe for many-to-one / one-to-many entity navigation properties (like EF Core).

Example : Returning Category with its Products, each Product has a reference back to Category.
Without IgnoreCycles, it throws: System.Text.Json.JsonException: A possible object cycle was detected
*/
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // to return enums as text
});

//add settings - configuration
builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));

var uploadsPath = Path.Combine(Environment.CurrentDirectory, "Uploads");
AppSettings.UploadsFolderPath = uploadsPath;

///
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // "Bearer"
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer("Api", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["AppSettings:Issuer"],
        ValidAudience = builder.Configuration["AppSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:JwtSecurityKey"] ?? throw new InvalidOperationException("JWT key is missing")))
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse();

            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";

            var result = JsonSerializer.Serialize(new
            {
                Status = false,
                StatusCode = 401,
                Message = "Unauthorized - Token is missing or invalid."
            });

            return context.Response.WriteAsync(result);
        }
    };
})
.AddCookie("AdminAuth", options =>
{
    options.LoginPath = "/dashboard/auth/login";
    options.AccessDeniedPath = "/dashboard/auth/access-denied";

}).AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
    options.CallbackPath = builder.Configuration["Authentication:Google:CallbackPath"]!;
}).AddFacebook(options =>
{
    options.ClientId = builder.Configuration["Authentication:Facebook:ClientId"]!;
    options.ClientSecret = builder.Configuration["Authentication:Facebook:ClientSecret"]!;
    options.CallbackPath = builder.Configuration["Authentication:Facebook:CallbackPath"]!;
});

// Add a CORS policy named "AllowMyFrontend"
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5141/") // Only allowed for this domain
    .AllowAnyHeader() // Allow all headers
    .AllowAnyMethod(); // Allow all HTTP Methods (GET, POST...)
    });
});

// add rate limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", limiterOptions =>
    {
        limiterOptions.Window = TimeSpan.FromSeconds(10);
        limiterOptions.PermitLimit = 5; // 5 requests per 10 seconds
    });
});

var app = builder.Build();

/*================== APPLY SECURITY ============================*/

// Add security headers
app.Use(async (context, next) =>
{
    // Prevents the browser from "guessing" or automatically changing the content type
    // if its .png but inside it js dont read it as js ,, prevent XSS
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    // If the browser detects XSS code, it blocks the page instead of executing the code 
    context.Response.Headers.Append("X-Frame-Options", "DENY");
    // csp policy , stop inline js , stop reading files from another domain , prevent XSS
    //context.Response.Headers.Append("Content-Security-Policy", "default-src 'self'");
    await next();
});

// CORS
//app.UseCors("AllowMyFrontend");

// Rate Limiting Middleware
app.UseRateLimiter(); // protect against DDos / Dos Attacks 

/*===============================================================*/

// add localization
app.UseRequestLocalization();
// custom exception middleware for api response to return custom message 
app.UseMiddleware<CustomExceptionMiddleware>();

// Configure the HTTP request pipeline. (list of middlewares)
// If not in dev, configure error handling and security headers
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// Redirects HTTP requests to HTTPS
app.UseHttpsRedirection();
// Serves files from wwwroot folder (css, js, images)
app.UseStaticFiles();

// Enables routing middleware to route HTTP requests -- this will create likw a map for all application routes
app.UseRouting();

// app.UseMiddleware<RedirectMiddleware>();

app.UseAuthentication();

// Adds authorization middleware (checks user permissions)
app.UseAuthorization();

// no pattern i dependent on attribute routing not conventional routing 
app.MapControllers();

// configure signalR 
app.MapHub<NotificationHub>("/notificationHub");

//default route (MinimalApi)
app.MapGet("/", () => "Home Page");

// app.MapControllerRoute(
//     name: "areas",
//     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// // 2. Your default route
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Runs the app and starts listening for HTTP requests -- what does it mean ?