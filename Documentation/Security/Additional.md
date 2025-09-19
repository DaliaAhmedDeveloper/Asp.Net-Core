# Secure Cookies
What it is:
Configure cookies to reduce risks of XSS, MITM (man-in-the-middle), and CSRF attacks.

Code:
```csharp
options.Cookie.HttpOnly = true; // JS can't access the cookie

/*
Only send over HTTPS -- Prevent MITM (man in the middle)
in http only the infromation send between browser and server without security
when we enable this means dont send cookies if connection is not secure 
*/
options.Cookie.SecurePolicy = CookieSecurePolicy.Always; 
 
// No cross-site cookie sending
// send cookies only for same domain
options.Cookie.SameSite = SameSiteMode.Strict;

```
Use this for session cookies, auth cookies, or any sensitive cookies.

# Dependency Updates & Vulnerability Scanning

What it is:
Use tools that automatically check your libraries (NuGet packages) for known security flaws.
GitHub Dependabot :  Auto-suggests updates in PRs.
OWASP Dependency-Check : Scans your code for vulnerable dependencies.

Is it important?
Yes , Medium/High Priority.
Especially useful in long-running projects. Prevents attacks from outdated libraries.

# API Security Best Practices

What it is:
For APIs (especially public ones), apply:

- API versioning (/api/v1/...) to manage updates safely.
- Rate limiting : to prevent abuse (already in your list).
- API keys/IP whitelisting : for internal/private APIs.

Is it important?
Yes , High Priority for public APIs.

For internal apps, API keys or IP whitelisting can be optional, but helpful.

# Security Testing Tools

What it is:
- ZAP (OWASP) :  Free tool to scan your app for security holes.
- Burp Suite : Powerful web security scanner (free + paid).
- dotnet-outdated : CLI to find outdated .NET packages.

Is it important?
Yes , Useful in CI/CD or before releases.
You don't need to run them daily, but before deploying to production or during code audits, they're extremely helpful.

# OWASP Top 10 Awareness

What it is:
OWASP publishes a list of top 10 most common/critical web security risks, like:

OWASP 2023 Risk	Example
Broken Access Control	            : A user accessing another user’s data
Cryptographic Failures	            : Storing passwords in plaintext
Injection (SQL, NoSQL)              : Already covered
Insecure Design	                    : Poorly planned architecture
Security Misconfiguration           : Leaving debug mode on in production
Vulnerable & Outdated Components	: Using old libraries with bugs
Identification & Auth Failures	    : Weak login logic
Data Integrity Failures             : Tampered cookies
Logging & Monitoring Failures    	: No alert when breach happens
SSRF (Server-Side Request Forgery)	: Backend fetches unsafe URLs

Is it important?
Yes : Critical Awareness.
You don't need to memorize all 10, but every dev should be aware of these general risks.