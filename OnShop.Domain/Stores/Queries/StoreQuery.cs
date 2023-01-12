using System.Collections.Generic;
using MediatR;
using OnShop.Domain.DTOs;
using OnShop.Domain.Stores.Dtos;

namespace OnShop.Domain.Stores.Queries
{
    public class StoreQuery : IRequest<IReadOnlyList<StoreDto>>
    {
        
    }
}