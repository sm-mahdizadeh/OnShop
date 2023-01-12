using System.Collections.Generic;
using MediatR;
using OnShop.Domain.DTOs;
using OnShop.Domain.Product.Dtos.Brands;

namespace OnShop.Domain.Product.Queries.Brands
{
    public class GetBrandQueries : BaseQueries, IRequest<IReadOnlyList<GetBrandDto>>
    {

    }
}