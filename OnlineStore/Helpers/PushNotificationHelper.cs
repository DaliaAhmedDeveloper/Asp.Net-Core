using Microsoft.AspNetCore.SignalR;
using OnlineStore.Hubs;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Services;
namespace OnlineStore.Helpers;

public class PushNotificationHelper
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IUserService _user;
    public PushNotificationHelper(IHubContext<NotificationHub> hubContext, IUserService user)
    {
        _hubContext = hubContext;
        _user = user;
    }
    public async Task PushToAdmins(NotificationDto notification)
    {
        // Get all admin user IDs from database
        var admins = await _user.GetAdmins();
        // Send notification to each admin
        foreach (var admin in admins)
        {
            // push notification
            await _hubContext.Clients.User(admin.Id.ToString()).SendAsync("ReceiveNotification", notification);
        }
    }
    public async Task PushToAll(NotificationDto notification)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
    }
    public async Task PushToUser(int userId , NotificationDto notification)
    {
       await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveNotification", notification);
    }
}