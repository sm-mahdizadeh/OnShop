using OnShop.Domain.Interfaces;
using OnShop.Domain.Payments.Entities;

namespace OnShop.Domain.Payments.Repositories
{
    public interface IPaymentRepository : IAsyncRepository<Payment>
    {
        
    }
}