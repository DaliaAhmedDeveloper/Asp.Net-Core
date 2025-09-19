# Social Login & Signup using Google or Facebook in ASP.NET Core

This guide explains how to integrate Google and Facebook login in your ASP.NET Core application. 
It covers both login and signup logic.

## Step 1: Install NuGet packages:
dotnet add package Microsoft.AspNetCore.Authentication.Facebook --version 8.0.8
dotnet add package Microsoft.AspNetCore.Authentication.Google --version 8.0.8

## Step 2: Configure Authentication in `Program.cs`

```csharp
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle("Google", options =>
{
    options.ClientId = "YOUR_GOOGLE_CLIENT_ID";
    options.ClientSecret = "YOUR_GOOGLE_CLIENT_SECRET";
    options.CallbackPath = "/login-google";
})
.AddFacebook("Facebook", options =>
{
    options.AppId = "YOUR_FACEBOOK_APP_ID";
    options.AppSecret = "YOUR_FACEBOOK_APP_SECRET";
    options.CallbackPath = "/login-facebook";
});
```
## Step 3: Create Login Endpoints (Google / Facebook)

```csharp
[Route("login-google")]
public IActionResult LoginWithGoogle()
{
    var props = new AuthenticationProperties { RedirectUri = "/external-login-callback" };
    return Challenge(props, "Google");
}

[Route("login-facebook")]
public IActionResult LoginWithFacebook()
{
    var props = new AuthenticationProperties { RedirectUri = "/external-login-callback" };
    return Challenge(props, "Facebook");
}
```
## Step 4: Handle the External Login Callback

```csharp
[Route("external-login-callback")]
public async Task<IActionResult> ExternalLoginCallback()
{
    var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    var claims = result.Principal.Identities.FirstOrDefault()?.Claims;

    var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
    var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

    if (string.IsNullOrEmpty(email))
        return BadRequest("Email not found");

    // Check if user exists in the database
    var user = await _userService.GetUserByEmailAsync(email);

    if (user == null)
    {
        // Signup: Create new user
        user = new User
        {
            Email = email,
            Name = name,
            Provider = "Google or Facebook",
            CreatedAt = DateTime.UtcNow
        };
        await _userService.CreateUserAsync(user);
    }

    // Login: Generate JWT or start session
    var token = _jwtService.GenerateToken(user);

    return Ok(new { token });
}
```

---

## Step 5: Add User Table Example (EF Core)

```csharp
public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Provider { get; set; } // "Google", "Facebook"
    public DateTime CreatedAt { get; set; }
}
```

---

## Step 6: Create Token Service (JWT Example)

```csharp
public string GenerateToken(User user)
{
    var claims = new[]
    {
        new Claim(ClaimTypes.Name, user.Name),
        new Claim(ClaimTypes.Email, user.Email)
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: "yourdomain.com",
        audience: "yourdomain.com",
        claims: claims,
        expires: DateTime.Now.AddHours(1),
        signingCredentials: creds);

    return new JwtSecurityTokenHandler().WriteToken(token);
}
```

# Steps to Generate OAuth Keys for Google and Facebook

- Step 1: Go to Google Cloud Console
  Open Google Cloud Console , Sign in with your Google account.

- Step 2: Create a new project (or select existing)
  Click Select a project (top-left).
  Click New Project, enter a name, then Create.

- Step 3: Enable Google+ API (optional, for profile info)
  Navigate to APIs & Services > Library.
  Search for Google People API or Google+ API.
  Click Enable.

- Step 4: Create OAuth 2.0 Credentials
  Go to APIs & Services > Credentials.
  Click Create Credentials > OAuth client ID.
  Configure consent screen if prompted (fill app info).
  Choose Web Application.
  Enter a name (e.g., MyApp Web Client).
  Under Authorized redirect URIs, add your callback URL:
  Example: https://localhost:5001/signin-google
  Click Create.

- Step 5: Copy your Client ID and Client Secret
  Save these securely.
  Use these in your ASP.NET Core app configuration.

# Facebook App Credentials

- Step 1: Go to Facebook Developers site
  Open Facebook for Developers
  Log in with your Facebook account.

- Step 2: Create a new app
  Click My Apps > Create App.
  Choose Consumer app type and click Next.
  Enter your app name and contact email, then Create App.

- Step 3: Add Facebook Login product
  In your app dashboard, click Add Product > Facebook Login.
  Choose Web as platform.

- Step 4: Configure Facebook Login Settings
  Enter your site URL (e.g., https://localhost:5001).
  In Settings > Facebook Login, set Valid OAuth Redirect URIs:
  Example: https://localhost:5001/signin-facebook
  Save changes.

- Step 5: Get App ID and App Secret
  From the Dashboard page, copy your App ID and App Secret.
  Use these in your ASP.NET Core app configuration.
