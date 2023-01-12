using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnShop.ApplicationServices.Specifications.Orders;
using OnShop.Domain.DTOs;
using OnShop.Domain.Orders.Dtos;
using OnShop.Domain.Orders.Entities;
using OnShop.Domain.Orders.Queries;
using OnShop.Domain.Orders.Repositories;
using OnShop.Framework.Common;

namespace OnShop.ApplicationServices.Orders.Queries
{
    public class OrderQueryHandler :
        IRequestHandler<GetOrderByIdQueries, OrderDto>,
        IRequestHandler<GetOrderByUserIdQueries, QueryList<OrderDto>>,
        IRequestHandler<GetOrderDetailsQueries, QueryList<OrderDetailsDto>>
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderDetailRepository _detailRepository;

        public OrderQueryHandler(IOrderRepository repository, IOrderDetailRepository detailRepository)
        {
            _repository = repository;
            _detailRepository = detailRepository;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQueries request, CancellationToken cancellationToken)
        {
            var result = await _repository.FirstOrDefaultAsync(new OrderSpecification(request.OrderId));
            if (result != null)
            {
                return new OrderDto()
                {
                    Amount = result.OrderDetails.Sum(x => x.FinalPrice),
                    Id = result.Id
                };
            }

            return null;
        }

        public async Task<QueryList<OrderDto>> Handle(GetOrderByUserIdQueries request, CancellationToken cancellationToken)
        {
            var spec = new OrderSpecification(request.UserId);
            var count = await _repository.CountAsync(spec);
            var res = await _repository.GetPagedRespondAsync(spec, request.PageSize, request.Skip);
            var lst = res?.Select(Mapper).ToList().AsReadOnly();
            return new QueryList<OrderDto> { Data = lst, TotalCount = count };
        }

        private OrderDto Mapper(Order order)
        {
            return new OrderDto()
            {
                Id = order.Id,
                CreatorUserId = order.CreatorUserId,
                CreateDate = order.CreateDate,
                IsRemoved = order.IsRemoved,
                ModifiedId = order.ModifiedId,
                ModifiedDate = order.ModifiedDate,
                RemoveTime = order.RemoveTime,
                OrderDate = order.CreateDate,
                Amount = order.OrderDetails.Sum(x => x.FinalPrice),
                OrderState = order.OrderState.GetEnumDisplayName()

            };
        }

        private OrderDetailsDto MapperDetails(OrderDetail orderDetail)
        {
            return new OrderDetailsDto()
            {
                OrderId = orderDetail.OrderId,
                Count = orderDetail.Count,
                CategoryName = orderDetail.Product.Category?.Title,
                FinalPrice = orderDetail.FinalPrice,
                ProductImage = orderDetail.Product.ProductImages.FirstOrDefault()?.Src,
                ProductTitle = orderDetail.Product.Title,
                ProductId = orderDetail.ProductId


            };
        }

        public async Task<QueryList<OrderDetailsDto>> Handle(GetOrderDetailsQueries request, CancellationToken cancellationToken)
        {
            var spec = new OrderDetailsSpecification(request.OrderId);
            var count = await _detailRepository.CountAsync(spec);
            var res = await _detailRepository.ListAsync(spec);
            var lst = res?.Select(MapperDetails).ToList().AsReadOnly();
            return new QueryList<OrderDetailsDto> { Data = lst, TotalCount = count };
        }
    }
}