namespace OnlineStore.Middlewares;

// to create custom middleware create new class and follow same structure below 
// register the middleware in program.cs 

// public delegate Task RequestDelegate(HttpContext context); the built-in delegate returns task and take http request
using Microsoft.Extensions.Options;
public class RedirectMiddleware // create new class for the custom middleware
{
    private readonly RequestDelegate _next;  // built-in delegate 
    private readonly AppSettings _settings;

    public RedirectMiddleware(RequestDelegate next, IOptions<AppSettings> options) // its delegate
    {
        _next = next; // assign next to _next
        _settings = options.Value;
    }
    public async Task InvokeAsync(HttpContext context) // its task because delegate returns task so i have to await it
    {
        // Code before next middleware
        //Console.WriteLine("Before next middleware");

        // check if mentinance mode is on inside settings then redirect to website under mentainance 
        if (_settings.MentainanceMode == "On" && !context.Request.Path.Value!.ToLower().Contains("/home/mentainane"))
        {
            context.Response.Redirect("/Home/Mentainane");
            return;
        }

        await _next(context);  // Call next middleware -- call the method inside the delegate - send http request as parameter

        // Code after next middleware
        Console.WriteLine("After next middleware");
    }
}
