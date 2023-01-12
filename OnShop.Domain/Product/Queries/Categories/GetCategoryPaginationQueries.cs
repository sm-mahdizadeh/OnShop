using MediatR;
using OnShop.Domain.DTOs;
using OnShop.Domain.Product.Dtos.Categories;

namespace OnShop.Domain.Product.Queries.Categories
{
    public class GetCategoryPaginationQueries : BaseQueries, IRequest<QueryList<GetCategoryDto>>
    {

    }
}