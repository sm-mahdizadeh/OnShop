using System;
using MediatR;
using OnShop.Domain.Basket.Dtos;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Basket.Queries
{
    public class GetCardQueries : IRequest<ResultDto<CartDto>>
    {
        public Guid? BrowserId { get; set; }
        public int? UserId { get; set; }
    }

    public class GetCardShippingQueries : IRequest<ResultDto<CartPayDto>>
    {
        public Guid? BrowserId { get; set; }
        public int? UserId { get; set; }
    }
}