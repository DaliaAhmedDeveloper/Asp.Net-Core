namespace  OnlineStore.Providers;
using Microsoft.AspNetCore.Authorization; // namespace of all authorization classes and interfaces
using Microsoft.EntityFrameworkCore; // ef framework to be able to use linq with dbcontext
using Microsoft.Extensions.Options; // get authorization options 

// with IAuthorizationPolicyProvider interface u have to declare the 3 methods below
public class DynamicAuthorizationPolicyProvider : IAuthorizationPolicyProvider
{
    // this class contains  GetDefaultPolicyAsync and GetFallbackPolicyAsync
    private readonly DefaultAuthorizationPolicyProvider _fallbackPolicyProvider; 
    private readonly IServiceScopeFactory _scopeFactory;

    public DynamicAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options,IServiceScopeFactory scopeFactory)
    {
        _fallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        _scopeFactory = scopeFactory;
    }

    // default if u just use [Authorize]
    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        return _fallbackPolicyProvider.GetDefaultPolicyAsync();
    }
    // if you dont use any [authorize]
    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
    {
        return _fallbackPolicyProvider.GetFallbackPolicyAsync();
    }

    // EX: [Authorize(Policy = "update-user")]
    public async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        using var scope = _scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // check if update-user inside permissions table
        var permissionExists = await db.Permissions.AnyAsync(p => p.Slug == policyName);

        // if yes add policy to claim
        if (permissionExists)
        {
            var policy = new AuthorizationPolicyBuilder();
            policy.RequireClaim("Permission", policyName);
            return policy.Build();
        }
        // if no work as no authorize
        return await _fallbackPolicyProvider.GetPolicyAsync(policyName);
    }
}
