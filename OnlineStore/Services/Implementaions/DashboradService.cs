using OnlineStore.Models.ViewModels;
using OnlineStore.Repositories;

namespace OnlineStore.Services;

public class DashboardService : IDashboardService
{
    private readonly IUnitOfWork _unitOfWork;
    public DashboardService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DashboardViewModel> Dashboard()
    {
        // orders count this month 
        // users this month
        // total count of orders
        // total count of users
        // latest 7 orders 
        // total income 
        // total support tickets 
        // support tickets this month
        // total products
        // latest 5 products
        // latest 4 users

        var model = new DashboardViewModel
        {
            OrderCount = await _unitOfWork.Order.CountAllAsync(),
            OrderCountCurrentMonth =  await _unitOfWork.User.CountCurrentMonthAsync(),
            UserCount = await _unitOfWork.User.CountAllAsync(),
            UserCountCurrentMonth = await _unitOfWork.User.CountCurrentMonthAsync(),
            LatestUsers =await _unitOfWork.User.GetLatestAsync(),
            ProductCount = await _unitOfWork.Product.CountAllAsync(),
            CouponCount = await _unitOfWork.Coupon.CountAllAsync(),
            WarehouseCount = await _unitOfWork.Warehouse.CountAllAsync(),
            SupportTicketsCount = await _unitOfWork.SupportTicket.CountAllAsync(),
            OrderIncomeCurrentMonth = await _unitOfWork.Order.TotalIncomeCurrentMonth(),
            OrderTotalIncome = await _unitOfWork.Order.TotalIncome(),
            LatestOrders =  await _unitOfWork.Order.GetLatestAsync(),
            LatestProducts = await _unitOfWork.Product.GetLatestAsync(),
            ReturnCount = await _unitOfWork.Return.CountAllAsync() ,
        };
        return model;
    }
}