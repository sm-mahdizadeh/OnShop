using MediatR;
using OnShop.Domain.DTOs;
using OnShop.Domain.Stores.Dtos;

namespace OnShop.Domain.Stores.Queries
{
    public class StorePaginationQuery : BaseQueries, IRequest<QueryList<StoreDto>>
    {
        
    }
}