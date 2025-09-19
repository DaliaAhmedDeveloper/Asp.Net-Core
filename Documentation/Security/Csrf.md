# What is CSRF?

CSRF (Cross-Site Request Forgery) is an attack where a malicious website tricks a logged-in user into making an unwanted action on another website without their knowledge.

## The danger: The request comes from the user’s browser, so it looks legit to your app — but the user never meant to do it.

## Real Example

Imagine you're logged into your banking website:

POST https://mybank.com/transfer
Body: amount=1000&to=attacker_account
Then, while logged in, you visit an evil site containing this HTML:

```html
<form action="https://mybank.com/transfer" method="POST">
  <input type="hidden" name="to" value="attacker_account">
  <input type="hidden" name="amount" value="1000">
  <input type="submit">
</form>
<script>document.forms[0].submit();</script>
```
## What happens?

Since you're already logged in to mybank.com, your browser sends cookies automatically.
The form is submitted without your consent.
Money is transferred to the attacker — because it looks like you submitted the request.

## Key Point

CSRF uses the victim's session (cookies) against them.
It exploits trust your app has in the logged-in user.

# How to Prevent CSRF
In ASP.NET Core MVC or Razor Pages: Enable CSRF protection (it’s on by default) for forms:

```html
<form method="post">
    @Html.AntiForgeryToken()
</form>
```
And in the controller:

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Submit(MyModel model) { ... }
This ensures the form includes a hidden token, and only valid tokens will pass.

In APIs:
Frontend will send the token with each request 

# What if You Don’t Prevent It?
Attackers can:

- Change your user’s password or email
- Submit fake forms
- Delete their account
- Trigger sensitive actions silently

