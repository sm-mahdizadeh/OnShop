using MediatR;
using OnShop.Domain.Product.Dtos.Categories;
using System.Collections.Generic;

namespace OnShop.Domain.Product.Queries.Categories
{
    public class GetCategoryQueries : IRequest<IReadOnlyList<GetCategoryDto>>
    {

    }
}