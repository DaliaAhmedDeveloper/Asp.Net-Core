# Microsoft.AspNetCore.Http;
## Purpose:
   Provides classes to handle HTTP requests and responses in ASP.NET Core.
   Examples:
   - HttpContext — represents the current HTTP request/response context.
   - HttpRequest — access to the incoming HTTP request.
   - HttpResponse — access to the outgoing HTTP response.
   - IFormFile — represents uploaded files in a request.

```csharp
public async Task InvokeAsync(HttpContext context)
{
    var userAgent = context.Request.Headers["User-Agent"].ToString();
    await context.Response.WriteAsync($"Your browser is: {userAgent}");
}
```

# Microsoft.IdentityModel.Tokens;
## Purpose:
   Contains tools to create, validate, and manipulate security tokens (like JWTs).
   Examples:
   - SecurityToken — base class for security tokens.
   - SymmetricSecurityKey — represents a symmetric encryption key.
   - TokenValidationParameters — settings used to validate tokens.

```csharp
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key_here"));
var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
```

# Microsoft.Extensions.Options;
## Purpose:
   Helps manage application settings and configuration using the Options pattern.
   Examples:
   - IOptions<T> — interface to access configured options.
   - OptionsMonitor<T> — monitors options for changes.
   - Configure<T>() — method to bind configuration to classes.

```csharp
public class MyService
{
    private readonly MySettings _settings;
    public MyService(IOptions<MySettings> options)
    {
        _settings = options.Value;
    }
    public void PrintSiteName() => Console.WriteLine(_settings.SiteName);
}
```

# Microsoft.AspNetCore.Identity;
## Purpose:
   Provides a framework for user authentication, authorization, and identity management.
   Examples:
   - UserManager<TUser> — manages users, passwords, roles, claims.
   - SignInManager<TUser> — handles sign-in operations.
   - IdentityUser — default user entity class.

```csharp
// Register a new user using UserManager
var user = new IdentityUser { UserName = "user@example.com", Email = "user@example.com" };
var result = await userManager.CreateAsync(user, "P@ssw0rd!");
if(result.Succeeded)
{
    Console.WriteLine("User created successfully.");
}

```
# Microsoft.AspNetCore.Mvc;
## Purpose:
   Core MVC framework for building web apps and APIs.
   Examples:
   - Controller — base class for MVC controllers.
   - ActionResult — base class for action results returned by controller actions.
   - HttpGetAttribute — attribute to specify an action handles HTTP GET requests.

```csharp
// Simple MVC Controller with an action
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}

```

# Microsoft.AspNetCore.Mvc.Abstractions;
## Purpose:
   Contains abstract base classes and interfaces used internally by MVC.
   Examples:
   - ActionDescriptor — describes an MVC action.
   - ControllerActionDescriptor — describes a controller action.
   - ParameterDescriptor — describes action parameters.

``` csharp
// Create an ActionDescriptor instance (usually used internally)
var actionDescriptor = new ActionDescriptor
{
    DisplayName = "SampleAction",
    RouteValues = { { "controller", "Home" }, { "action", "Index" } }
};
Console.WriteLine(actionDescriptor.DisplayName);
```

# Microsoft.AspNetCore.Mvc.ModelBinding;
## Purpose:
   Handles binding incoming request data (query, form, route) to C# models.
   Examples:
   - ModelBinder — base class for custom model binders.
   - ModelStateDictionary — stores validation state of model binding.
   - BindAttribute — attribute to specify binding behavior on action parameters.

```csharp
```

# Microsoft.AspNetCore.Mvc.Razor;
## Purpose:
   Supports the Razor view engine for rendering dynamic HTML views.
   Examples:
   - IRazorViewEngine — interface to find and render Razor views.
   - RazorPage<TModel> — base class for Razor pages with a model.
   - RazorView — represents a compiled Razor view.

```csharp
// Use IRazorViewEngine to find a view (simplified)
public class MyService
{
    private readonly IRazorViewEngine _viewEngine;
    public MyService(IRazorViewEngine viewEngine) { _viewEngine = viewEngine; }
    
    public void FindViewExample(ActionContext context)
    {
        var result = _viewEngine.FindView(context, "Index", false);
        if(result.Success) Console.WriteLine("View found!");
    }
}

```

# Microsoft.AspNetCore.Mvc.Rendering;
## Purpose:
   Provides helpers to generate HTML elements programmatically in views.
   Examples:
   - HtmlHelper — helps render HTML controls.
   - SelectList — represents a list of items for <select> dropdowns.
   - TagBuilder — builds HTML tags in code.

```csharp
// Build a dropdown list in a Razor view
@{
    var items = new SelectList(new[] { "Red", "Green", "Blue" });
}
@Html.DropDownList("Colors", items)

```

# Microsoft.AspNetCore.Mvc.ViewFeatures;
## Purpose:
   Contains features used by views, like view data and temp data.
   Examples:
   - ViewDataDictionary — dictionary to pass data from controller to view.
   - TempDataDictionary — dictionary to pass temporary data between requests.
   - HtmlHelper — helper methods to render HTML inside views.

``` csharp 
// Using ViewData and TempData in a controller
public IActionResult Index()
{
    ViewData["Message"] = "Hello from ViewData!";
    TempData["Notice"] = "This is a one-time message.";
    return View();
}

```

# Microsoft.AspNetCore.Routing;
## Purpose:
   Handles routing: mapping URLs to controllers/actions.
   Examples:
   - RouteData — contains route values for the current request.
   - RouteBuilder — helps build route templates.
   - Endpoint — represents an endpoint in the routing system.

```csharp
// Access route data in a controller action
public IActionResult Show()
{
    var id = RouteData.Values["id"];
    return Content($"Route ID is: {id}");
}

```

# Microsoft.Extensions.Localization;
## Purpose:
   Supports localization and internationalization (multi-language support).
   Examples:
   - IStringLocalizer — interface to retrieve localized strings.
   - IStringLocalizerFactory — creates localizers.
   - ResourceManagerStringLocalizer — implementation that uses resource files.

```csharp
// Use IStringLocalizer for localization
public class HomeController : Controller
{
    private readonly IStringLocalizer<HomeController> _localizer;
    public HomeController(IStringLocalizer<HomeController> localizer)
    {
        _localizer = localizer;
    }

    public IActionResult Index()
    {
        var message = _localizer["WelcomeMessage"];
        return Content(message);
    }
}

```

# Microsoft.EntityFrameworkCore;
## Purpose:
   The Entity Framework Core ORM for working with databases using C# classes.
   Examples:
   - DbContext — main class for querying and saving data.
   - DbSet<TEntity> — represents a table in the database.
   - Migration — used to apply schema changes to the database.

```csharp
// Define a DbContext and use it to query data
public class MyDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
}

var context = new MyDbContext();
var products = await context.Products.ToListAsync();
```

# Microsoft.AspNetCore.Authentication.JwtBearer;
## Purpose:
   Middleware to authenticate requests using JWT bearer tokens.
   Examples:
   - JwtBearerDefaults.AuthenticationScheme — default scheme name.
   - JwtBearerEvents — events for customizing JWT handling.
   - Extension methods like AddJwtBearer() to register JWT auth.

```csharp
// In Startup.cs: Add JWT Authentication
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "yourissuer",
            ValidateAudience = true,
            ValidAudience = "youraudience",
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key"))
        };
    });

```
# Microsoft.AspNetCore.RateLimiting;
## Purpose:
   Middleware to limit how many requests clients can make in a time window.
   Examples:
   - RateLimiterOptions — configure rate limiting rules.
   - ConcurrencyLimiter — limits concurrent requests.
   - TokenBucketRateLimiter — implements token bucket algorithm.

```csharp
// Simple example to limit requests (in Program.cs)
app.UseRateLimiter(new RateLimiterOptions
{
    OnRejected = (context, token) =>
    {
        context.HttpContext.Response.StatusCode = 429;
        return new ValueTask();
    }
});

```