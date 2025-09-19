# using System;

Purpose: The base namespace for fundamental .NET types like Console, Math, DateTime, Exception.
Example: Using DateTime and Math

```csharp
using System;

Console.WriteLine(DateTime.Now);
Console.WriteLine(Math.Sqrt(16)); // 4
```

# using System.Security.Claims;

Purpose: Deals with user identity and authentication claims (like username, email, roles).
A claim is a piece of information about the user.

Example: Creating a user identity with claims

```csharp
using System.Security.Claims;

var claims = new List<Claim>
{
    new Claim(ClaimTypes.Name, "John Doe"),
    new Claim(ClaimTypes.Email, "john@example.com"),
    new Claim(ClaimTypes.Role, "Admin")
};

var identity = new ClaimsIdentity(claims, "CustomAuth");
Console.WriteLine($"User: {identity.Name}, Role: {identity.FindFirst(ClaimTypes.Role)?.Value}");
```
# using System.IO;

Purpose: For working with files, directories, streams.
Example: Writing and reading a file

```csharp
using System.IO;

// Write
File.WriteAllText("test.txt", "Hello File!");

// Read
string content = File.ReadAllText("test.txt");
Console.WriteLine(content);
```
# using System.Threading.Tasks;
# using System.Text;

Purpose: Handles text encoding, string manipulation (StringBuilder).
Example: Using StringBuilder

``` csharp
using System.Text;

var sb = new StringBuilder();
sb.Append("Hello");
sb.Append(" World");
Console.WriteLine(sb.ToString()); // Hello World
```
# using System.IdentityModel.Tokens.Jwt;

Purpose: Work with JSON Web Tokens (JWT) for authentication.
Example: Reading JWT token claims

```csharp
using System.IdentityModel.Tokens.Jwt;

var handler = new JwtSecurityTokenHandler();
var token = handler.ReadJwtToken("your.jwt.token.here");
foreach (var claim in token.Claims)
{
    Console.WriteLine($"{claim.Type}: {claim.Value}");
}
```
# using System.Security.Cryptography;

Purpose: Encrypting, hashing, generating keys.
Example: Hashing a password with SHA256

```csharp

using System.Security.Cryptography;
using System.Text;

string password = "secret";
using var sha256 = SHA256.Create();
byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

Console.WriteLine(Convert.ToBase64String(hash));
```
# using System.Text.Json;

Purpose: Working with JSON (serialization/deserialization).
Example: Convert object to JSON

``` csharp
using System.Text.Json;

var person = new { Name = "Alice", Age = 25 };
string json = JsonSerializer.Serialize(person);
Console.WriteLine(json);

var deserialized = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
Console.WriteLine(deserialized["Name"]); // Alice
```
# using System.Collections.Generic;

Purpose: Provides generic collections (List<T>, Dictionary<TKey,TValue>).
Example: Using a list

```csharp
using System.Collections.Generic;

List<int> numbers = new() { 1, 2, 3 };
numbers.Add(4);
Console.WriteLine(string.Join(", ", numbers));
```
# using System.Net.Mail;

Purpose: Sending emails via SMTP.
Example: Sending an email

``` csharp

using System.Net.Mail;

// Make sure to replace with valid SMTP settings
var mail = new MailMessage("from@example.com", "to@example.com", "Test Subject", "Hello Email!");
var client = new SmtpClient("smtp.example.com")
{
    Port = 587,
    Credentials = new System.Net.NetworkCredential("username", "password"),
    EnableSsl = true
};
```
