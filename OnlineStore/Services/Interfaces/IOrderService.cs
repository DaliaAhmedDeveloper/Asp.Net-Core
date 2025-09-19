namespace OnlineStore.Services;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Models.ViewModels;
using OnlineStore.Models.Dtos.Responses;

public interface IOrderService
{
    Task<IEnumerable<Order>> List();
    Task<OrderDetailsDto?> Find(int id);
    Task<OrderDto> Create(CreateOrderDto dto, int userId);
    Task<IEnumerable<OrderListDto>> ListAllByUser(int userId);
    //get orders inside this mounth 
    Task<IEnumerable<Order>> ListOrdersCurrentMonth();
    Task<OrderTrackingDto> TrackOrder(string orderNumber);

    // web
    Task<Order?> GetForWeb(int id);
    Task<PagedResult<Order>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber, int pageSize);
    Task<IEnumerable<Order>> GetAllForWeb();
    Task<Order> UpdateForWeb(OrderViewModel model, Order Order);
    Task<bool> DeleteForWeb(int id);
    Task<Order> UpdateStatus(Order order, EditOrderViewModel dto);
    Task<bool> CancelOrder(int orderId);
}