namespace OnlineStore.Areas.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using OnlineStore.Services;
using OnlineStore.Helpers;
using OnlineStore.Models.ViewModels;
using OnlineStore.Models.Enums;
using OnlineStore.Models.Dtos.Responses;

[Area("Api")]
[Route("[area]/test")]
public class TestController : ControllerBase
{
    private readonly IEmailService _email;
    private readonly ICartService _cart;
    private readonly PushNotificationHelper _push;
    public TestController( IEmailService email, ICartService cart, PushNotificationHelper push)
    {
        _email = email;
        _cart = cart;
        _push = push;

    }
    // test email 
    [HttpGet("email")]
    public async Task<IActionResult> TestEmail()
    {
        string rootPath = Directory.GetCurrentDirectory();

        var id = AuthHelper.GetAuthenticatedUserId(HttpContext);
        var cart = await _cart.FindByUserId(id);
        if (cart == null) throw new NotFoundException("Cart is not found");
        var model = new AbandonedCartEmailModel
        {
            CustomerName = "dolly",
            CartUrl = "https://yourstore.com/cart/123",
            Items = cart.Items
        };
        var templateService = new EmailTemplateService();
        string htmlBody = await templateService.RenderAsync("Emails/AbandonedCart", model);
        await _email.SendEmailAsync("daliah20244@gmail.com", "You Left SomeThing In Your Cart!", htmlBody);
        return Ok("Email sent successfully");
    }

    [HttpGet("signalr/{type}")]
    public async Task<IActionResult> SignalR(string type)
    {
        var notification = new NotificationDto
        {
            Type = NotificationType.Info,
            Url = "dashboard/orders",
            Title = "Notification title",
            Message = "Message ",
        };

        try
        {
            if (type == "admins")
            {
                await _push.PushToAdmins(notification);
                return Ok("Notification Pushed to admins");
            }
            await _push.PushToAll(notification);
            return Ok("Notification Pushed to all");
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }

    }
    
}