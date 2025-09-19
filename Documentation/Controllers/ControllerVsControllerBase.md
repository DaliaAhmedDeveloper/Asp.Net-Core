/*
[ApiController] enhances controller behavior for APIs:
- Automatically infers [FromBody], [FromQuery], [FromRoute]

in mvc : 

[FromForm] is default for Http post request

[FromQuery] is default for simple types in GETs

[FromRoute] is used automatically if the parameter is in the URL

[FromBody] is rarely used — only when posting JSON manually (like with JavaScript fetch or AJAX)

In Api with [ApiController]

[FromForm] Rarely used, not default in APIs

[FromQuery] is default for simple types in GETs

[FromRoute] is used automatically if the parameter is in the URL

[FromBody] is the default of http requests with formbody as json

- Returns 400 Bad Request if model is invalid or body is missing
  if you want to return error messages instead you have to turn this feature of using 
  
```csharp
  builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
   ```
  and use model state
- Improves Swagger support
- Reduces need to check ModelState.IsValid manually

without [ApiController] like in mvc:
- You must specify [FromBody], etc. when u need to use 
- You must check ModelState.IsValid manually	
- missing body value Action executes with null model

In Case you:
Use [FromBody] manually, and
Want full control over model validation and error responses,
then you do NOT need [ApiController].

public class ProductController : ControllerBase {}

ControllerBase is a base class for Web API controllers in ASP.NET Core.
It provides the core features needed to handle HTTP API requests — like:
- Ok(), BadRequest(), NotFound(), etc.
- Access to Request, Response, User, and ModelState also : controller can do

# Example for set request header values : 
string userAgent = Request.Headers["User-Agent"];
string searchTerm = Request.Query["q"];

# Example for access response header : 
Response.Headers.Add("X-Custom-Header", "MyValue");
Response.StatusCode = 200;

# Example to work with user: 
```csharp
string username = User.Identity?.Name;
bool isAuthenticated = User.Identity?.IsAuthenticated ?? false;
bool isAdmin = User.IsInRole("Admin");
```

# Example of model state:
```csharp
if (!ModelState.IsValid)
{
    return BadRequest(ModelState); // or return custom error
}
```

- It does NOT include support for Razor Views or View rendering (like View()).

All points controller also can do even working with json 
but the main difference is controllerBase is working with json only no views
*/