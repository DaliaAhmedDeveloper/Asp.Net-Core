## Security Points :

1. Authentication
Use JWT Tokens or OAuth2.
Make sure to protect your endpoints with [Authorize].
Store secret keys (such as the JWT key) in appsettings.json or Secret Manager – not in your code.

2. Authorization
Use Roles or Policies to define permissions.
[Authorize(Roles = "Admin")]

3. SQL Injection
Don't manually write SQL in your code.
Always use EF Core with parameterized queries.

4. Cross-Site Scripting (XSS)
Don't print user data in HTML pages without sanitizing it.
In API only: Make sure you don't return scripts from the data.
When using Razor Pages or Blazor:
@Html.Encode(model.UserInput)

5. Cross-Site Request Forgery (CSRF)
For MVC or Razor Pages: Use an AntiForgery Token.
@Html.AntiForgeryToken()
In Web APIs: CSRF is not required if the API is protected with a Token.

6. Input Validation
Use FluentValidation or DataAnnotations.
Don't assume the user or frontend will send valid data.

7. Exception Handling
Create a dedicated middleware to handle exceptions.
Don't return a StackTrace in the Response.

Make sure there is a unified message for the user and a detailed message for the logs.

8. Logging and Monitoring
Log all errors using ILogger or Serilog.
Hide sensitive data from logs (such as passwords).

9. Exposing Sensitive Data
When your API or backend returns data (e.g., JSON), never return the full Entity Model that contains sensitive fields like passwords or private information.
Instead, use DTOs (Data Transfer Objects) that include only the safe fields you want to expose.

10. Rate Limiting & Throttling
Protect the API from attacks like brute-force using rate limiting.
You can use:
AspNetCoreRateLimit

```csharp
public class SimpleRateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly ConcurrentDictionary<string, ClientRequestStats> Clients = new();
    private readonly int _maxRequests;
    private readonly TimeSpan _timeWindow;

    public async Task InvokeAsync(HttpContext context)
    {
        var clientIp = context.Connection.RemoteIpAddress?.ToString();
        var stats = Clients.GetOrAdd(clientIp, _ => new ClientRequestStats());

        lock (stats)
        {
            if (DateTime.UtcNow - stats.WindowStart > _timeWindow)
            {
                stats.Count = 0;
                stats.WindowStart = DateTime.UtcNow;
            }

            if (++stats.Count > _maxRequests)
            {
                context.Response.StatusCode = 429;
                await context.Response.WriteAsync("Too many requests");
                return;
            }
        }
        await _next(context);
    }
}
```

11. HTTPS Only
Use HTTPS only.

Enable HTTPS redirection in Program.cs:
```csharp
app.UseHttpsRedirection();
app.UseHsts(); // HTTP Strict Transport Security
```
12. Security Headers

Add headers like:
X-Content-Type-Options: nosniff
X-XSS-Protection: 1; mode=block
Strict-Transport-Security

You can use middleware like:
NWebsec.AspNetCore.Middleware

```csharp
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Append("X-Frame-Options", "DENY");
    context.Response.Headers.Append("Content-Security-Policy", "default-src 'self'");
    await next();
});
```

13. CORS Configuration
Limit the sites allowed to access the API:

```csharp
services.AddCors(options =>
{
    options.AddPolicy("ProductionPolicy", builder =>
    {
        builder.WithOrigins("https://trusted-domain.com")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

app.UseCors("ProductionPolicy");
```

14. File Upload Security
Check the file type and size.
Save files in a non-executable folder (no .exe, no .cshtml, etc.).

15. Password Storage
Store passwords using a hashing like BCrypt or PBKDF2.
Never store your password as plain text.

16. Brute force protection :

Brute force attacks happen when an attacker tries many password (or token) guesses rapidly to break into an account.
Brute force protection means stopping or slowing down these repeated login attempts to prevent account compromise.

Use Recaptcha for brute force protection