namespace OnlineStore.Services.BackgroundServices;
using OnlineStore.Services;

public class CartReminderService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    public CartReminderService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var _email = scope.ServiceProvider.GetRequiredService<IEmailService>();
                var _language = scope.ServiceProvider.GetRequiredService<ILanguageService>();
                // var cartsToNotify = _context.Carts
                // .Where(c => c.Items.Any() && c.UpdatedAt <= DateTime.UtcNow.AddHours(-1))
                // .Select(c => new
                // {
                //     UserEmail = c.User.Email,
                //     UserId = c.UserId,
                //     UserName = c.User.FullName,

                //     Items = c.Items.Select(i => new CartItem
                //     {
                //         Id = i.Id,
                //         Quantity = i.Quantity,
                //         Product = i.Product
                //     }).ToList()
                // })
                // .ToList();

                // foreach (var cart in cartsToNotify)
                // {
                //     // Send email and Firebase notification here
                //     int userId = cart.UserId;
                //     var model = new  AbandonedCartEmailModel
                //     {
                //         CustomerName = cart.UserName,
                //         CartUrl = "https://yourstore.com/cart/123",
                //         Items = cart.Items
                //     };
                //     string htmlBody = await _render.RenderViewToStringAsync("/Views/Emails/AbandonedCart.cshtml", model);
                //     await _email.SendEmailAsync(cart.UserEmail, "You Left SomeThing In Your Cart!", htmlBody);
                // }
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
}