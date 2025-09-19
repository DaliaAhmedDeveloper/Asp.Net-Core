## _authorizationService.AuthorizeAsync : 
```csharp
CanAddUser = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "user.add")).Succeeded
```

_authorizationService.AuthorizeAsync(...) → asks the Authorization Service if the current user is allowed to perform an action (in this case, "user.add").

UserClaimsPrincipal → represents the current logged-in user with all their claims (identity info, roles, permissions, etc.).

null → is the resource being secured. Here you are checking just a policy/requirement, not a specific object.

"user.add" → is the policy name you configured (likely in Program.cs or Startup.cs).

.Succeeded → returns true if the user is authorized, otherwise false