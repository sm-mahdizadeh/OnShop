using System;
using MediatR;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Basket.Commands
{
    public class RemoveFromCardCommand : IRequest<ResultDto>
    {
        public long CartItemId { get; set; }
        public long ProductId { get; set; }
        public Guid? BrowserId { get; set; }
        public int? UserId { get; set; }
    }
}