# Steps For Jwt Authentication : 

- install the package 

  for asp.net 8 run the command 
  dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.7

- Add jwt Configuration inside program.cs

```csharp
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // issuer is the entity or system that created (issued) the token.
            ValidateIssuer = true,
            // Audience is the entity or application that is allowed to use the token.
            ValidateAudience = true,
            // Expiration time
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourdomain.com",
            ValidAudience = "yourdomain.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
        };
    });
```
- Create JwtAuthenticationHelper class

```csharp
public static class JwtAuthenticationHelper
{
    public static string GenerateJwtToken(string email)
{
    // this is the key using for token signature
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345")); // store in config!
    // tell jwt which security key to use and which algorithm
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    // create user claims 
    var claims = new[]
    {
        new Claim(ClaimTypes.Email, email),
        new Claim(ClaimTypes.Role, "User") // optional
    };

    // Generate the JWT token
    var token = new JwtSecurityToken(
        issuer: "yourdomain.com",          // The issuer (who created the token)
        audience: "yourdomain.com",        // The audience (who the token is meant for)
        claims: claims,                    // The user data or claims inside the token payload
        expires: DateTime.Now.AddHours(1),// Token expiration time (valid for 1 hour)
        signingCredentials: credentials   // The signing credentials used to create the token signature
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}

}
```

- use it inside login action 

```csharp
 public IActionResult SignIn(LoginViewModel model)
    {
        User? user = _user.CheckUser(model.Email, model.Password , UserType.User);
        if (user != null)
        {
            var token = GenerateJwtToken(model.Email);
            return Ok(new { token });
        }
        return Unauthorized();
    }
```
# What is the token and how it generated ?

JWT token is a string of characters that proves who you are and lets you access protected resources
without sending your password every time.

The token consists of three parts separated by dots (.):

header.payload.signature

- The header contains information about the token type and signature method.

- The payload contains data (claims) such as the username, expiration date, issuer, audience, etc.

- A signature is a digital signature created using a secret key and an encryption method (e.g., HMAC SHA256).

# How jwt check the token send from postman

- When the server receives the token from the client (for example, in an authorization header), it:

- Separates the token's components (header, payload, signature).

- It takes the sender's header and payload and calculates the signature again using the same algorithm and secret key.

- It compares the calculated signature with the signature sent within the token.

- If the signature matches, the token has not been tampered with and is considered valid.

- If the signature differs, the token is rejected because it has been altered or the secret key is incorrect.