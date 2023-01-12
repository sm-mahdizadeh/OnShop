using OnShop.Domain.Interfaces;
using OnShop.Domain.Orders.Entities;

namespace OnShop.Domain.Orders.Repositories
{
    public interface IOrderDetailRepository : IAsyncRepository<OrderDetail>
    {
        
    }
}