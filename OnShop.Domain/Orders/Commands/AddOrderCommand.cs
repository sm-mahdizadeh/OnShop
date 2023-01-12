using MediatR;
using OnShop.Domain.Enum;
using OnShop.Domain.Orders.Dtos;
using OnShop.Framework.Commands;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Orders.Commands
{
    public class AddOrderCommand : BaseCommandEntity,  IRequest<ResultDto<OrderDto>>
    {
        public long UserAddressId { get; set; }
        public OrderPostType OrderPostType { get; set; }
        public OrderState OrderState { get; set; }
    }


}