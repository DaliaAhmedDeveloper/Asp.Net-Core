# Send Emails Via Mailtrap

To connect Mailtrap with an ASP.NET Core 8 project (ASP.NET Core 8.0), you'll configure the SMTP settings provided by Mailtrap and send emails using the built-in SmtpClient or a mail library like MailKit (recommended for modern .NET apps).

## Recommended: Use MailKit
ASP.NET 8 does not include a built-in SMTP client anymore (SmtpClient is outdated), so the best practice is to use MailKit.

## Step-by-Step: Connect Mailtrap with ASP.NET 8 using MailKit

1. Install MailKit via NuGet
Use the terminal or Package Manager Console:
```bash
dotnet add package MailKit
```
2. Mailtrap SMTP Credentials

Login to Mailtrap.io, go to Inbox > SMTP Settings and copy your credentials:
Host: sandbox.smtp.mailtrap.io
Port: 587 (or 2525)
Username: your_mailtrap_username
Password: your_mailtrap_password

3. Add Settings to appsettings.json
```json
"EmailSettings": {
  "SmtpServer": "sandbox.smtp.mailtrap.io",
  "Port": 587,
  "Username": "your_mailtrap_username",
  "Password": "your_mailtrap_password",
  "FromName": "Online Store",
  "FromEmail": "noreply@onlinestore.com"
}
```
4. Create a Mail Service Class
```csharp
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;

public class EmailSettings
{
    public string SmtpServer { get; set; } = "";
    public int Port { get; set; }
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string FromName { get; set; } = "";
    public string FromEmail { get; set; } = "";
}

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string body);
}

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = body
        };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_settings.SmtpServer, _settings.Port, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_settings.Username, _settings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
```
🔹 5. Register in Program.cs
``` csharp
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddTransient<IEmailService, EmailService>();
```
🔹 6. Usage in Controller or Service
``` csharp
public class AccountController : ControllerBase
{
    private readonly IEmailService _email;

    public AccountController(IEmailService email)
    {
        _email = email;
    }

    [HttpPost("test-email")]
    public async Task<IActionResult> SendTestEmail()
    {
        await _email.SendEmailAsync("test@example.com", "Welcome!", "<h1>Welcome to our store</h1>");
        return Ok("Email sent successfully!");
    }
}
```
Output
This will send your test email through Mailtrap, and you can see it in the Mailtrap inbox dashboard.

## Security Tip

Do not hardcode credentials. Use Secret Manager or environment variables in production:

dotnet user-secrets set "EmailSettings:Username" "yourusername"
dotnet user-secrets set "EmailSettings:Password" "yourpassword"


================================================================

# Send Emails As Html : 

## Option 1: Use Razor View as Email Template (Recommended for complex emails)

Steps:
1. Create a Razor View (not tied to a Controller)
e.g. Views/Emails/WelcomeEmail.cshtml

```html
@model WelcomeEmailModel

<h2>Welcome, @Model.UserName!</h2>
<p>Thanks for joining our platform. We're excited to have you.</p>
<p><a href="@Model.ConfirmLink">Click here to confirm your email</a></p>
```
2. Render the view to a string
You’ll need a helper to render Razor Views to string (outside of a controller). Here's a minimal service:

```csharp

public interface IViewRenderService
{
    Task<string> RenderToStringAsync<TModel>(string viewName, TModel model);
}

public class ViewRenderService : IViewRenderService
{
    private readonly IRazorViewEngine _viewEngine;
    private readonly ITempDataProvider _tempDataProvider;
    private readonly IServiceProvider _serviceProvider;

    public ViewRenderService(IRazorViewEngine viewEngine,
        ITempDataProvider tempDataProvider,
        IServiceProvider serviceProvider)
    {
        _viewEngine = viewEngine;
        _tempDataProvider = tempDataProvider;
        _serviceProvider = serviceProvider;
    }

    public async Task<string> RenderToStringAsync<TModel>(string viewName, TModel model)
    {
        var actionContext = new ActionContext(
            new DefaultHttpContext { RequestServices = _serviceProvider },
            new RouteData(),
            new ActionDescriptor());

        var viewResult = _viewEngine.FindView(actionContext, viewName, false);
        if (!viewResult.Success)
            throw new InvalidOperationException($"Couldn't find view '{viewName}'");

        var viewDictionary = new ViewDataDictionary<TModel>(
            new EmptyModelMetadataProvider(),
            new ModelStateDictionary())
        { Model = model };

        using var sw = new StringWriter();
        var viewContext = new ViewContext(
            actionContext,
            viewResult.View,
            viewDictionary,
            new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
            sw,
            new HtmlHelperOptions());

        await viewResult.View.RenderAsync(viewContext);
        return sw.ToString();
    }
}
```
3. Register the service in DI:

```csharp
builder.Services.AddScoped<IViewRenderService, ViewRenderService>();
```
4. Use it when sending emails:

```csharp
var htmlBody = await _viewRenderService.RenderToStringAsync("Emails/WelcomeEmail", model);
await _emailService.SendEmailAsync(user.Email, "Welcome!", htmlBody);

```
## Option 2: Manually add HTML as a string
Still valid, but harder to manage and maintain:

```csharp
var body = $@"
  <h2>Welcome, {user.Name}!</h2>
  <p>Thanks for registering. Please confirm your email by clicking below.</p>
  <a href='{confirmationLink}'>Confirm Email</a>";

  ```
Use this only for very simple one-off emails.