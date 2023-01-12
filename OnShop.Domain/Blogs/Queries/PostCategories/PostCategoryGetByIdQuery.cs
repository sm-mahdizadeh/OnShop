using MediatR;
using OnShop.Domain.Blogs.Dtos;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Blogs.Queries.PostCategories
{
    public class PostCategoryGetByIdQuery : IRequest<ResultDto<PostCategoryGetQueryDto>>
    {
        public long Id { get; set; }
}
}
