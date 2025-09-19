namespace OnlineStore.Models.ViewModels;
public class DashboardViewModel
{
    public int OrderCount { get; set; }
    public int OrderCountCurrentMonth { get; set; }
    public int UserCount { get; set; }
    public int UserCountCurrentMonth { get; set; }
    public int ProductCount { get; set; }
    public int ReturnCount { get; set; }
    public int CouponCount { get; set; }
    public int WarehouseCount { get; set; }
    public int SupportTicketsCount { get; set; }
    public decimal OrderTotalIncome { get; set; }
    public decimal OrderIncomeCurrentMonth { get; set; }
    public IEnumerable<Product> LatestProducts { get; set; } = new List<Product>();
    public IEnumerable<Order> LatestOrders { get; set; } = new List<Order>();
    public IEnumerable<User> LatestUsers { get; set; } = new List<User>();
}