using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using OnlineStore.Services;

namespace OnlineStore.Repositories;

public class PaymentRepository : GenericRepository<Payment>,  IPaymentRepository
{
    public PaymentRepository(AppDbContext context) :base(context)
    {
    }

}
