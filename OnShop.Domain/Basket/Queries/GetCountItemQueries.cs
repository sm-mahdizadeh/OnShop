using System;
using MediatR;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Basket.Queries
{
    public class GetCountItemQueries : IRequest<ResultDto<int>>
    {
        public Guid? BrowserId { get; set; }
        public int? UserId { get; set; }
    }
}