namespace OnlineStore.Models.ViewModels;

using Microsoft.AspNetCore.Authorization;

public class SidebarViewModel
{
    // Users
    public bool CanAddUser { get; set; }
    public bool CanListUser { get; set; }
    public bool CanListRole { get; set; }

    // Categories
    public bool CanAddCategory { get; set; }
    public bool CanListCategory { get; set; }

    // Products
    public bool CanAddProduct { get; set; }
    public bool CanListProduct { get; set; }
    public bool CanListTag { get; set; }
    public bool CanListReview { get; set; }
    public bool CanListAttribute { get; set; }
    public bool CanListAttributeValue { get; set; }

    // Orders
    public bool CanListOrder { get; set; }

    // Notifications
    public bool CanListNotification { get; set; }

    // Countries/States/Cities
    public bool CanAddCountry { get; set; }
    public bool CanListCountry { get; set; }
    public bool CanListState { get; set; }
    public bool CanListCity { get; set; }

    // Coupons
    public bool CanAddCoupon { get; set; }
    public bool CanListCoupon { get; set; }

    // Warehouses
    public bool CanAddWarehouse { get; set; }
    public bool CanListWarehouse { get; set; }

    // Support Tickets
    public bool CanShowSupportTicket { get; set; }
    public bool CanListSupportTicket { get; set; }

    // Returns
    public bool CanListReturn { get; set; }

    // Invoices
    public bool CanListInvoice { get; set; }

    // Logs
    public bool CanListLog { get; set; }

    // Settings
    public bool CanListSettings { get; set; }

    // Add more permissions here if needed
}
