using OnShop.DAL.Context;
using OnShop.DAL.Data;
using OnShop.Domain.Orders.Entities;
using OnShop.Domain.Orders.Repositories;

namespace OnShop.DAL.Orders.Repositories
{
    public class OrderDetailRepository : EfRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}