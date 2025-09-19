using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using OnlineStore.Services;

namespace OnlineStore.Repositories;

public class ShippingMethodRepository : GenericRepository<ShippingMethod>,  IShippingMethodRepository
{
    public ShippingMethodRepository(AppDbContext context) :base(context)
    {
    }

}
