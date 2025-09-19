namespace OnlineStore.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Helpers;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using System.Threading.Tasks;

[Area("Dashboard")]
[Authorize(AuthenticationSchemes = "AdminAuth")]
[Route("dashboard/order")]
public class OrderController : Controller
{
    private readonly IOrderService _order;
    private readonly AppSettingHelper _setting;
    public OrderController(IOrderService order, AppSettingHelper setting)
    {
        _order = order;
        _setting = setting;
    }

    // GET: dashboard/order
    [HttpGet]
    [Authorize(Policy = "order.list")]
    public async Task<IActionResult> Index(int? page, string searchTxt)
    {
        int pageNumber = page ?? 1;
        var results = await _order.GetAllWithPaginationForWeb(searchTxt, pageNumber, 10);

        return View(results);
    }

    // GET: dashboard/order/5
    [HttpGet("{id}")]
    [Authorize(Policy = "order.show")]
    public async Task<IActionResult> Details(int id)
    {
        var order = await _order.GetForWeb(id);
        if (order == null)
            return NotFound();

        // Map to view model
        var model = new OrderViewModel
        {
            ReferenceNumber = order.ReferenceNumber,
            UserName = order.User.FullName,
            TotalAmountBeforeSale = order.TotalAmountBeforeSale,
            TotalAmountAfterSale = order.TotalAmountAfterSale,
            SaleDiscountAmount = order.SaleDiscountAmount,
            FinalAmount = order.FinalAmount,
            OrderStatus = order.OrderStatus,
            OrderItems = order.OrderItems,
            PaymentMethod = order.PaymentMethod,
            Coupon = order.Coupon?.Code ?? "",
            CouponDiscountAmount = order.CouponDiscountAmount,
            PointsDiscountAmount = order.PointsDiscountAmount,
            PointsUsed = order.PointsUsed,
            CashFees = decimal.TryParse(await _setting.GetValue("cash_on_delivery_fees"), out var fee) ? fee : 0m,
            WalletAmountUsed = order.WalletAmountUsed,
            ShippingAddress = order.ShippingAddress.FullName + ", " + order.ShippingAddress.Street + ", " + order.ShippingAddress.City + ", " + order.ShippingAddress.Country,
            ShippingMethod = order.ShippingMethod.Name,
            ShippingMethodCost = order.ShippingMethod.Cost,
            Payment = order.Payment != null ? $"Amount: {order.Payment.Amount}, Date: {order.Payment.PaymentDate}" : string.Empty
        };

        return View(model);
    }

    // GET: dashboard/order/edit/5
    [HttpGet("edit/{id}")]
    [Authorize(Policy = "order.update")]
    public async Task<IActionResult> Edit(int id)
    {
        var order = await _order.GetForWeb(id);
        if (order == null)
            return NotFound();

        var model = new EditOrderViewModel
        {
            OrderId = id,
            ReferenceNumber = order.ReferenceNumber,
            OrderStatus = order.OrderStatus,
        };
        return View(model);
    }

    // POST: dashboard/order/5
    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "order.update")]
    public async Task<IActionResult> Edit(EditOrderViewModel model, int id)
    {
        var order = await _order.GetForWeb(id);
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        if (order == null)
            return NotFound();

        await _order.UpdateStatus(order, model);
        TempData["SuccessMessage"] = "Order updated successfully!";
        return View(model);
    }

    // POST: dashboard/order/delete/5
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "order.delete")]
    public async Task<IActionResult> Delete(int id)
    {
        bool record = await _order.DeleteForWeb(id);
        if (!record)
            return NotFound();

        TempData["SuccessMessage"] = "Order deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
