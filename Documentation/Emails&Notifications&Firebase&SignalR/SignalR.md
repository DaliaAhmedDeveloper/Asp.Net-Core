# What is SignalR?

SignalR is a real-time communication library for ASP.NET Core that allows server and clients (browser, desktop, mobile) to send/receive data instantly—without refreshing the page.

## Typical use cases:

- Chat applications

- Live notifications (e.g., new order, message, alert)

- Real-time dashboards (stock prices, IoT data, etc.)

- Collaborative apps (like Google Docs)

## Steps to Use SignalR in ASP.NET Core

1. Install SignalR
   dotnet add package Microsoft.AspNetCore.SignalR

2. Create a NotificationHub inside Hubs folder
   This hub will push messages to connected clients.

```csharp
using Microsoft.AspNetCore.SignalR;

public class NotificationHub : Hub
{
    // Optional: send message to specific user
    public async Task SendNotification(string userId, string message)
    {
        await Clients.User(userId).SendAsync("ReceiveNotification", message);
    }
}
```
3. Register SignalR in Program.cs

```csharp
builder.Services.AddSignalR();
app.MapHub<NotificationHub>("/notificationHub");
```

npm install @microsoft/signalr  should i install this

you should install @microsoft/signalr if your frontend (browser, React, Vue, or plain HTML/JS) needs to connect to your SignalR hub. This package provides the JavaScript/TypeScript client for SignalR.


Or if you are using CDN / plain HTML, you can skip npm and include via script:

<script src="https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/7.0.5/signalr.min.js"></script>
<script>
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();
      connection.on("ReceiveNotification", (notification) => {
      console.log("Pushing Notification Strat", notification);
    });
    connection.start().catch(err => console.error(err));
<script>


Hub = server-side endpoint managing all real-time connections

client = client means Browser tab that connect to the Hub

## Methods  Sending Messages with Clients

Clients lets you target specific sets of clients:
--------------------------------------------------------------------------
Method	                            Sends to
--------------------------------------------------------------------------
Clients.All	                        All connected clients. all browser tabs
Clients.Caller	                    The client that invoked the Hub method.
Clients.Others	                    All clients except the caller.
Clients.Client(connectionId)	    A specific connection.
Clients.Clients(connectionIds)	    Multiple specific connections.
Clients.User(userId)	            All connections for a specific user.
Clients.Users(userIds)	            All connections for multiple users.
Clients.Group(groupName)	        All connections in a group.
Clients.Groups(groupNames)	        All connections in multiple groups.
Clients.OthersInGroup(groupName)	All group members except the caller.


## Sending SignalR notifications to a specific user using userId

To send messages to a specific logged-in user in ASP.NET Core 8 SignalR, follow these steps:

1. Store the user ID in claims
   When the user logs in, add their unique ID to a claim, typically using ClaimTypes.NameIdentifier.

Example:
```csharp
var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    new Claim(ClaimTypes.Email, user.Email),
    new Claim("UserType", UserType.Admin.ToString())
};
```

2. Create a custom user ID provider
   Implement IUserIdProvider to tell SignalR which claim to use as the user identifier:
   signalR by default compare with claim called NameIdentifire if you dont have this claim name 
   or you want to use another one you have to create the provider and specify the claim to use 

```csharp
public class CustomUserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        return connection.User?.FindFirst(ClaimTypes.Id)?.Value ?? null!;
    }
}
```
3. Register the provider in DI
```csharp
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
```

4. Secure the Hub with authentication

Use [Authorize] on the Hub and specify your authentication scheme:
```csharp
[Authorize(AuthenticationSchemes = "AdminAuth")] // if you use scheme
[Authorize(AuthenticationSchemes = "AdminAuth,UserAuth")] // you can use this if you want to apply two schemes or you can create a hub for each one
public class NotificationHub : Hub { }
```
5. Connect from the client with cookies
   Ensure the browser sends authentication cookies to SignalR:

<script>
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub", { withCredentials: true })
    .build();
</script>

6. Send notifications to a specific user
   From the server, use Clients.User(userId) to target that user across all their active tabs/devices:

```csharp
await _hubContext.Clients.User(targetUserId)
    .SendAsync("ReceiveNotification", notification);
```
