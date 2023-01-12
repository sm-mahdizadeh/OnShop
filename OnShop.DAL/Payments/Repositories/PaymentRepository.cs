using OnShop.DAL.Context;
using OnShop.DAL.Data;
using OnShop.Domain.Payments.Entities;
using OnShop.Domain.Payments.Repositories;

namespace OnShop.DAL.Payments.Repositories
{
    public class PaymentRepository : EfRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}