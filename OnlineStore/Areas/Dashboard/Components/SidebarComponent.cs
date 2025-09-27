using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Helpers;
using OnlineStore.Models.ViewModels;

public class SidebarViewComponent : ViewComponent
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<SidebarViewComponent>  _logger;

    public SidebarViewComponent(IAuthorizationService authorizationService,
                                IHttpContextAccessor httpContextAccessor, ILogger<SidebarViewComponent> logger)
    {
        _authorizationService = authorizationService;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        try
        {
            var model = new SidebarViewModel
            {
                // Users
                CanAddUser = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "user.add")).Succeeded,
                CanListUser = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "user.list")).Succeeded,
                CanListRole = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "role.list")).Succeeded,

                // Categories
                CanAddCategory = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "category.add")).Succeeded,
                CanListCategory = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "category.list")).Succeeded,

                // Products
                CanAddProduct = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "product.add")).Succeeded,
                CanListProduct = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "product.list")).Succeeded,
                CanListTag = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "tag.list")).Succeeded,
                CanListReview = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "review.list")).Succeeded,
                CanListAttribute = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "attribute.list")).Succeeded,
                CanListAttributeValue = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "attributeValue.list")).Succeeded,

                // Orders
                CanListOrder = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "order.list")).Succeeded,
                // Shipping Method
                CanListShippingMethod = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "shippingMethod.list")).Succeeded,

                // Notifications
                CanListNotification = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "notification.list")).Succeeded,

                // Locations
                CanAddCountry = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "country.add")).Succeeded,
                CanListCountry = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "country.list")).Succeeded,
                CanListState = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "state.list")).Succeeded,
                CanListCity = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "city.list")).Succeeded,

                // Coupons
                CanAddCoupon = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "coupon.add")).Succeeded,
                CanListCoupon = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "coupon.list")).Succeeded,

                // Warehouses
                CanAddWarehouse = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "warehouse.add")).Succeeded,
                CanListWarehouse = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "warehouse.list")).Succeeded,

                // Support Tickets
                CanShowSupportTicket = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "supportTicket.show")).Succeeded,
                CanListSupportTicket = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "supportTicket.list")).Succeeded,

                // Returns
                CanListReturn = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "return.list")).Succeeded,

                // Invoices
                CanListInvoice = true,

                // Logs
                CanListLog = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "logs.list")).Succeeded,

                // Settings
                CanListSettings = (await _authorizationService.AuthorizeAsync(UserClaimsPrincipal, null, "settings.list")).Succeeded
            };
            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _logger.LogErrorWithTranslations( // log to the database
                ex,
               "Sidebar Component Error", // concise, professional title
               "An unexpected error occurred while rendering the sidebar. Please try again or contact support.",
               "خطأ في مكون الشريط الجانبي",
               "حدث خطأ غير متوقع أثناء عرض الشريط الجانبي. يرجى المحاولة مرة أخرى أو الاتصال بالدعم.");

            return Content("");
        }
    }
}
