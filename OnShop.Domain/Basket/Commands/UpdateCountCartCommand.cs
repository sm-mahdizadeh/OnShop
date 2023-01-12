using System;
using MediatR;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Basket.Commands
{
    public class UpdateCountCartCommand : IRequest<ResultDto>
    {
        public int Count { get; set; }
        public long CartItemId { get; set; }
        public Guid? BrowserId { get; set; }
        public int? UserId { get; set; }
    }
    public class UpdateCartFinished : IRequest<ResultDto>
    {
        public long CartId { get; set; }
    }
}