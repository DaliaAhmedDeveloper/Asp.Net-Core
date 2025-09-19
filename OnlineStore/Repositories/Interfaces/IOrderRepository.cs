namespace OnlineStore.Repositories;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.Enums;
public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order?> GetWithItemsByIdAsync(int orderId);
    Task<bool> UpdateAsync(Order order, OrderStatus status);
    Task<OrderDetailsDto?> GetAsync(int id);
    Task<IEnumerable<OrderListDto>> GetAllByUserAsync(int userId);
    Task<IEnumerable<Order>> GetAllCurrentMonthAsync();
    Task<Order?> GetForWebWithRelationsAsync(int id);
    Task<Order?> GetByReferenceNumberAsync(string ReferenceNumber);
    Task<IEnumerable<Order>> GetAllWithPaginationAsync(string searchTxt, int page, int pageSize);
    Task<OrderItem?> GetOrderItemByIdAsync(int id);
    Task<bool> UpdateItemAsync(OrderItem entity);
    Task<decimal> TotalIncome();
    Task<decimal> TotalIncomeCurrentMonth();
}