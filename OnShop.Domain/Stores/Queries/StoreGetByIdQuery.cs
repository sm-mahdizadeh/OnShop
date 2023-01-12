using MediatR;
using OnShop.Domain.DTOs;
using OnShop.Domain.Stores.Dtos;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Stores.Queries
{
    public class StoreGetByIdQuery : IRequest<ResultDto<StoreDto>>
    {
        public long Id { get; set; }
    }
}