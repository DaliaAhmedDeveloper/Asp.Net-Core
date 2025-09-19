# Rate Limiting & Throttling Explained

## What is Rate Limiting?

Definition:
Rate limiting is a technique to control the number of requests a user/client can make to a server within a certain time window.

Purpose:
To prevent abuse like:

API flooding
Denial-of-Service (DoS) attacks
Excessive resource usage by a single client

Example:
Allow max 100 requests per minute per user or IP address.

How it works:
The server tracks incoming requests and rejects or delays requests once the limit is exceeded (usually with HTTP 429 status code).

## What is Throttling?

Definition:
Throttling is a broader term for controlling the usage of resources by slowing down or limiting request rates.

Difference from Rate Limiting:

Rate limiting is a strict limit on requests per time unit.
Throttling can include delaying requests or reducing bandwidth, not just outright blocking.

Example:
After a user reaches a certain request threshold, their requests may be delayed or served slower instead of blocked immediately.

## Why Use Them?

- Protect backend servers from overload.
- Ensure fair usage among users.
- Reduce risk of automated attacks and scraping.
- Improve overall system stability and reliability.

## How Are They Implemented?

- At the application level (e.g., middleware in ASP.NET Core).
- At the API gateway or reverse proxy (e.g., Nginx, Envoy).
- At the cloud provider or firewall (e.g., AWS API Gateway).


```csharp

using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
using System.Net;
using System.Threading.Tasks;

public class SimpleRateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly ConcurrentDictionary<string, ClientRequestStats> Clients 
        = new();

    private readonly int _maxRequests;
    private readonly TimeSpan _timeWindow;

    public SimpleRateLimitingMiddleware(RequestDelegate next, int maxRequests, TimeSpan timeWindow)
    {
        _next = next;
        _maxRequests = maxRequests;
        _timeWindow = timeWindow;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        var now = DateTime.UtcNow;

        var stats = Clients.GetOrAdd(clientIp, _ => new ClientRequestStats
        {
            Count = 0,
            WindowStart = now
        });

        lock (stats)
        {
            // Reset count if time window expired
            if (now - stats.WindowStart > _timeWindow)
            {
                stats.Count = 0;
                stats.WindowStart = now;
            }

            stats.Count++;

            if (stats.Count > _maxRequests)
            {
                // Rate limit exceeded
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests; // 429
                context.Response.ContentType = "text/plain";
                context.Response.Headers["Retry-After"] = (_timeWindow - (now - stats.WindowStart)).TotalSeconds.ToString("F0");
                await context.Response.WriteAsync("Too many requests. Please try again later.");
                return;
            }
        }

        await _next(context);
    }

    private class ClientRequestStats
    {
        public int Count { get; set; }
        public DateTime WindowStart { get; set; }
    }
}

// in program.cs
app.UseMiddleware<SimpleRateLimitingMiddleware>(maxRequests: 100, timeWindow: TimeSpan.FromMinutes(1));

```

Or 

``` csharp
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", limiterOptions =>
    {
        limiterOptions.Window = TimeSpan.FromSeconds(10);
        limiterOptions.PermitLimit = 5; // 5 requests per 10 seconds
    });
});

app.UseRateLimiter();
