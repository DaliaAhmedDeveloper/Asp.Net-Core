
# Localization in ASP.NET Core — Step-by-Step Guide

This guide explains how to add **multi-language support (localization)** to your ASP.NET Core project, enabling you to return messages in different languages based on user preferences or request headers.

## 1. Add Localization Services

In your `Program.cs` (or `Startup.cs`), register localization services and specify the resources folder path:

```csharp
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
```
## 2. Configure Supported Cultures

Configure the supported cultures and the default culture for your application:

```csharp
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("ar")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
```
## 3. Add Localization Middleware

In the `app` pipeline, add the localization middleware **before** other middlewares:

```csharp
var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

//OR 

app.UseRequestLocalization(); //IF you dont want to add options
```

## 4. Create Resource Files

- Create a folder named `Resources` in your project root.
- Inside `Resources`, create subfolders matching your namespaces (optional but recommended for organization).
- Add `.resx` resource files named after the class you want to localize.

Example:

Resources/
└── Middlewares/
    ├── CustomExceptionMiddleware.en.resx
    └── CustomExceptionMiddleware.ar.resx

## 5. Populate Resource Files

Each `.resx` file contains key-value pairs:

<?xml version="1.0" encoding="utf-8"?>
<root>
  <data name="ErrorMessage" xml:space="preserve">
    <value>Something Went Wrong Please Try Again Later !</value>
  </data>
</root>


## 6. Inject and Use `IStringLocalizer<T>`

In the class where you want localized strings (e.g., `CustomExceptionMiddleware`):

```csharp
using Microsoft.Extensions.Localization;

public class CustomExceptionMiddleware
{
    private readonly IStringLocalizer<CustomExceptionMiddleware> _localizer;

    public CustomExceptionMiddleware(IStringLocalizer<CustomExceptionMiddleware> localizer /* other dependencies */)
    {
        _localizer = localizer;
    }

    public async Task Invoke(HttpContext context)
    {
        var message = _localizer["ErrorMessage"];
        // Use the localized message...
    }
}
```

## 7. Specify the Culture in Requests

Clients can specify the preferred language by setting the HTTP header:

Accept-Language: ar
or
Accept-Language: en

The middleware will automatically detect and use this culture to return the localized strings.

## 8. Optional: Organize Resource Files with Namespace Matching

- For better organization, create subfolders under `Resources` that match your namespaces.
- Example:

Namespace: OnlineStore.Middlewares
Resource Path: Resources/Middlewares/CustomExceptionMiddleware.en.resx

This helps `IStringLocalizer<T>` find the correct resource files automatically.

## 9. Rebuild and Test

- Clean and rebuild your project.
- Test with different `Accept-Language` headers to confirm localization works.
