namespace OnlineStore.Helpers;

using Microsoft.AspNetCore.Identity;
public static class PasswordHelper
{
    public static string HashPassword(string password)
    {
        var hasher = new PasswordHasher<object>();
        return hasher.HashPassword(new object() , password);
    }
    public static bool VerifyPassword(string hashed, string input)
    {
        //throw new ArgumentException("Hashed password cannot be null or empty", nameof(hashed));
        var hasher = new PasswordHasher<object>();
        var result = hasher.VerifyHashedPassword(new object(), hashed, input);
        return result == PasswordVerificationResult.Success;
    }
}
