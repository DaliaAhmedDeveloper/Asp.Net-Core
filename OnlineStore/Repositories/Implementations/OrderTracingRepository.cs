
using OnlineStore.Models;

namespace OnlineStore.Repositories;

public class OrderTrackingRepository :GenericRepository<OrderTracking>,  IOrderTrackingRepository
{
    public OrderTrackingRepository(AppDbContext context) : base(context)
    {
    }   
}
