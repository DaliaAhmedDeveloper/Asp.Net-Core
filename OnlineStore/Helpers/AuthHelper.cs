using System.Security.Claims;

namespace OnlineStore.Helpers;

public static class AuthHelper
{
    public static int GetAuthenticatedUserId(HttpContext httpContext)
    {
        string? idString = httpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(idString, out int userId))
            throw new UnauthorizedAccessException("User ID is missing in claims.");

        return userId;        
    }
}