using MediatR;
using OnShop.Domain.DTOs;
using OnShop.Domain.Stores.Dtos;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Stores.Queries
{
    public class StoreGetByCodeQuery : IRequest<ResultDto<StoreDto>>
    {
        public string Code { get; set; }
    }
}