namespace OnlineStore.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

[Authorize(AuthenticationSchemes = "AdminAuth")]
public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        // var userId = Context.UserIdentifier;
        // Console.WriteLine($"Connected: {userId}");
        await base.OnConnectedAsync();
    }
}
