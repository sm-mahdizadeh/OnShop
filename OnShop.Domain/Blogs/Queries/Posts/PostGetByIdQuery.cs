using MediatR;
using OnShop.Domain.Blogs.Dtos;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Blogs.Queries.Posts
{
    public class PostGetByIdQuery: IRequest<ResultDto<PostGetQueryDto>>
    {
        public long Id { get; set; }
}
}
