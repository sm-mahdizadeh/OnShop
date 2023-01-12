using Ardalis.Specification;
using OnShop.Domain.Orders.Entities;

namespace OnShop.ApplicationServices.Specifications.Orders
{
    public sealed class OrderSpecification : Specification<Order>
    {
        public OrderSpecification(long id)
        {
            Query.Where(x => x.Id == id).Include(x => x.OrderDetails);
        }
        public OrderSpecification(long? cartId)
        {
            Query.Where(x => x.CartId == cartId).Include(x => x.OrderDetails);
        }

        public OrderSpecification(int userId)
        {
            Query.OrderByDescending(x => x.Id);
            Query.Where(x => x.CreatorUserId == userId).Include(x => x.OrderDetails);
        }

    }
}