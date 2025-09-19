# What is a Refresh Token?

A refresh token is a long-lived token issued alongside the JWT (access token).
Typically, the access token is short-lived (e.g., valid for 15 minutes or 1 hour).
After the access token expires, it can no longer be used to access APIs.
The refresh token allows the client to get a new access token without needing to log in again.

# Why do we need refresh tokens?

Security:
We set a short expiry time for the access token to reduce the risk if it gets stolen.

User convenience:
The user does not have to log in again every time the token expires.

Control:
We can revoke the refresh token (e.g., on logout) to force the user to log in again.

# How does the refresh token work?

When the user logs in, the server issues:

- An access token with a short lifespan.

- A refresh token with a longer lifespan (e.g., 7 days or more).

The client uses the access token to access protected APIs.

When the access token expires (e.g., after 15 minutes), the client sends the refresh token to the server to request a new access token.

- The server validates the refresh token (checks if it is valid and not revoked).

- If the refresh token is valid: The server issues a new access token.

  Optionally, it can issue a new refresh token and revoke the old one.

- If the refresh token is invalid or expired, the user must log in again.

# What happens on logout?

- The server revokes or deletes the refresh token associated with the user.

- Therefore, the refresh token cannot be used to get new access tokens.

- The user must log in again.


# Steps to Implement JWT + Refresh Token

1. Create Models
Create a model for refresh token (to store token info in DB):
```csharp
public class RefreshToken
{
    public int Id;
    public string Token { get; set; } = string.Empty;
    public int UserId { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsRevoked { get; set; }
    public User User { get; set; } = null!;
}

```
2. Generate JWT Access Token
Write a method to generate a short-lived JWT access token with claims:

```csharp
private string GenerateAccessToken(List<Claim> claims)
{
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key_here"));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: "yourdomain.com",
        audience: "yourdomain.com",
        claims: claims,
        expires: DateTime.Now.AddMinutes(15),
        signingCredentials: creds);

    return new JwtSecurityTokenHandler().WriteToken(token);
}
```
3. Generate Refresh Token
Create a method to generate a secure random refresh token:

```csharp
private RefreshToken GenerateRefreshToken(string userId)
{
    return new RefreshToken
    {
        Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
        UserId = userId,
        ExpiryDate = DateTime.Now.AddDays(7),
        IsRevoked = false
    };
}
```
4. Save Refresh Token
Save the generated refresh token to the database associated with the user.

5. Login Endpoint

On successful login:
Generate access token and refresh token.
Save refresh token in DB.
Return both tokens to client.

```csharp
[HttpPost("login")]
public IActionResult Login([FromBody] LoginRequest request)
{
    // Validate user credentials...

    var claims = new List<Claim> { new Claim(ClaimTypes.Name, request.Username) };

    var accessToken = GenerateAccessToken(claims);
    var refreshToken = GenerateRefreshToken(request.Username);

    // Save refreshToken in DB

    return Ok(new { accessToken, refreshToken = refreshToken.Token });
}
```
6. Refresh Token Endpoint
Client sends refresh token to get new access token:

```csharp
[HttpPost("refresh-token")]
public IActionResult RefreshToken([FromBody] string refreshToken)
{
    // Lookup refresh token in DB

    if (refreshToken == null || token is invalid or revoked or expired)
        return Unauthorized();

    // Generate new access token
    var claims = GetClaimsForUser(refreshToken.UserId);
    var newAccessToken = GenerateAccessToken(claims);

    // Optionally generate and save new refresh token ( but its more secure to mark this refresh token as revoke then generate new one or delete it to avoid lot of records inside database )

    return Ok(new { accessToken = newAccessToken /*, refreshToken = newRefreshToken */ });
}
```
7. Logout Endpoint
Revoke refresh token on logout:

```csharp
[HttpPost("logout")]
public IActionResult Logout([FromBody] string refreshToken)
{
    // Mark refresh token as revoked in DB or delete the record 

    return Ok();
}
```
Frontend developer (or frontend app) should call the refresh token API when the access token expires.