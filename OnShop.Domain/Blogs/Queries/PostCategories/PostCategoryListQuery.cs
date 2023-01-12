using MediatR;
using OnShop.Domain.Blogs.Dtos;
using OnShop.Domain.DTOs;

namespace OnShop.Domain.Blogs.Queries.PostCategories
{
    public class PostCategoryListQuery : BaseQueries, IRequest<QueryList<PostCategoryListQueryDto>>
    {
    }
}
