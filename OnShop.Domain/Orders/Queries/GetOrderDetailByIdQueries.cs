using MediatR;
using OnShop.Domain.DTOs;
using OnShop.Domain.Orders.Dtos;

namespace OnShop.Domain.Orders.Queries
{
    public class GetOrderByIdQueries : IRequest<OrderDto>
    {
        public long OrderId { get; set; }
    }
    public class GetOrderByUserIdQueries : BaseQueries, IRequest<QueryList<OrderDto>>
    {
        public int UserId { get; set; }
    }
    public class GetOrderDetailsQueries : BaseQueries, IRequest<QueryList<OrderDetailsDto>>
    {
        public long OrderId { get; set; }
    }
}