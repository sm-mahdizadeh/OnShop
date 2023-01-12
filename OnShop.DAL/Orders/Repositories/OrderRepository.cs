using OnShop.DAL.Context;
using OnShop.DAL.Data;
using OnShop.Domain.Orders.Entities;
using OnShop.Domain.Orders.Repositories;

namespace OnShop.DAL.Orders.Repositories
{
    public class OrderRepository : EfRepository<Order>, IOrderRepository
    {
        public OrderRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}