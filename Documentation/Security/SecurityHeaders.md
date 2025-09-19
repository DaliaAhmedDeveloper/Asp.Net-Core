# What are Security Headers?

They are headers sent in the server's HTTP response to the browser. Their purpose is to protect your site from common attacks such as:

- XSS (malicious JavaScript injection)
- MIME-type sniffing (unexpected file execution)
- HTTP attacks

## X-Content-Type-Options: nosniff

Function: Prevents the browser from "guessing" or automatically changing the content type.
Security benefit: Prevents malicious files uploaded with a safe extension but containing malicious content (such as uploading a .jpg containing JS code) from running.

✅ Always add this.

### Scenario without X-Content-Type-Options: nosniff:

Let's say the server sends a file named:
script.jpg and says its type is image/jpeg.

But the file actually contains malicious JavaScript code!

Some browsers (especially older ones) try to "understand" the content themselves...
They say: "It has an image on it, but it looks like JavaScript! I'll open it as JavaScript."
The result: The browser runs the malicious code contained in the file!

## X-XSS-Protection: 1; mode=block

Function: Activates the XSS filter within the browser.
Security benefit: If the browser detects XSS code, it blocks the page instead of executing the code.

⚠️ Note: This is only useful for older browsers. (Google Chrome and other modern browsers ignore it; it's best to use CSP instead.)

## Http Strict-Transport-Security (HSTS)

Function: Always forces the browser to use HTTPS, not HTTP.
Security benefit: Prevents man-in-the-middle attacks and forces secure browsing only.
Recommended by OWASP, especially when an SSL certificate is available.

Example:
Strict-Transport-Security: max-age=63072000; includeSubDomains; preload

### Scenario:

1. The user types in the browser:

http://example.com
The browser goes to the website, and the website redirects to HTTPS:  https://example.com
But the problem is that the first request (the one over HTTP) is unencrypted, giving a hacker an opportunity to infiltrate the script.

2. Here comes the HSTS:

After the first secure HTTPS visit, the server sends:
Strict-Transport-Security: max-age=31536000; includeSubDomains

Meaning:
"For one year (31536000 seconds), do not use anything other than HTTPS, even if the user types HTTP."
"And all subdomains must be HTTPS as well."
After that, the browser will never go to HTTP, and will automatically redirect any request to HTTPS before sending it.