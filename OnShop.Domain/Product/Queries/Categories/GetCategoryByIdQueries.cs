using MediatR;
using OnShop.Domain.Product.Dtos.Categories;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Product.Queries.Categories
{
    public class GetCategoryByIdQueries :  IRequest<ResultDto<GetCategoryDto>>
    {
        public long Id { get; set; }
    }
}