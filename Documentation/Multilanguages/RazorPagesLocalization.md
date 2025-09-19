# Switch styles based on language 

1. inside _Layout.cshtml

```cs
@using Microsoft.AspNetCore.Localization
@inject IHttpContextAccessor HttpContextAccessor

@{
    var culture = HttpContextAccessor.HttpContext?.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.Name;
    var isRtl = culture?.StartsWith("ar");
}
@if (isRtl == true){
    <link rel="stylesheet" href="~/dashboard/css/rtl.css" />
}
```

## Add Localization To Razor Pages :

1. inside resources folder create .resx files for views follow same namespace 

EX :
/Resources/Views/Home/Index.en.resx   -> English
/Resources/Views/Home/Index.ar.resx   -> Arabic

2. inside view use it :

@using Microsoft.AspNetCore.Mvc.Localization
@inject IViewLocalizer Localizer

<h1>@Localizer["WelcomeMessage"]</h1>
<p>@Localizer["WelcomeDescription"]</p>

3. Create Language Controller
```cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("lang")]
public class CultureController : Controller
{
    [HttpGet("{culture}")]
    public IActionResult SetLanguage(string culture)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );

        // Redirect back using Referer header
        var referer = Request.Headers["Referer"].ToString();
        if (!string.IsNullOrEmpty(referer) && Url.IsLocalUrl(referer))
        {
            return Redirect(referer);
        }

        // Fallback to home if Referer is not valid
        return RedirectToAction("Index", "Home");
    }
}
```
4. Add switching Button 
<li class="nav-item">
@if(CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "en"){
    <a class="nav-link" asp-area="Dashboard" asp-controller="Culture" asp-action="SetLanguage" asp-route-culture="ar">
        <i class="bi bi-globe"></i>
        <p>Arabic</p>
    </a>
}else{
    <a class="nav-link" asp-area="Dashboard" asp-controller="Culture" asp-action="SetLanguage" asp-route-culture="en">
        <i class="bi bi-globe"></i>
        <p>English</p>
    </a>
}
</li>


