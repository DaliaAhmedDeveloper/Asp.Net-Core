# What is CORS?

CORS = Cross-Origin Resource Sharing
It is a security mechanism in browsers that prevents a web page from accessing an API from a different domain unless the server explicitly allows it.

## Example:

If you have:

Frontend at: https://myfrontend.com
Backend API at: https://myapi.com

Request between the two different domains = "Cross-Origin Request"
Here, the browser denies access unless the API explicitly allows CORS.

# How to enable it in ASP.NET Core 8?

1. Add a CORS policy in Program.cs:

```csharp
var builder = WebApplication.CreateBuilder(args);
// Add a CORS policy named "AllowMyFrontend"
builder.Services.AddCors(options =>
{
options.AddPolicy("AllowMyFrontend", policy =>
{
policy.WithOrigins("https://myfrontend.com") // Only allowed for this domain
.AllowAnyHeader() // Allow all headers
.AllowAnyMethod(); // Allow all HTTP Methods (GET, POST...)
});
});

builder.Services.AddControllers();

```
2. Use the policy inside the middleware:

``` csharp
var app = builder.Build();
// Enable the CORS policy
app.UseCors("AllowMyFrontend");

```
# Important Notes:
Option Explanation
WithOrigins(...)    :  specifies which domains are allowed to access.
.AllowAnyMethod()   : allows all types of requests (GET, POST, PUT...).
.AllowAnyHeader()   : allows sending any custom headers, such as Authorization.
.AllowCredentials() : if you need to send cookies or tokens.

# Security Tip:
Do not write AllowAnyOrigin() in production!
This opens the API to any domain in the world, which is dangerous.