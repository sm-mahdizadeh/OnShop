using Ardalis.Specification;
using OnShop.Domain.Orders.Entities;

namespace OnShop.ApplicationServices.Specifications.Orders
{
    public sealed class OrderDetailsSpecification : Specification<OrderDetail>
    {
        public OrderDetailsSpecification(long orderId)
        {
            Query.Where(x => x.OrderId == orderId);
            Query.Include(x => x.Product).ThenInclude(x => x.ProductImages);
            Query.Include(x => x.Product).ThenInclude(x => x.Category);
        }
    }
}