using MediatR;
using OnShop.Domain.Blogs.Dtos;
using OnShop.Domain.DTOs;

namespace OnShop.Domain.Blogs.Queries.Posts
{
    public class PostPaginationQueries : BaseQueries, IRequest<QueryList<PostListQueryDto>>
    {
        public int? CategoryId { get; set; }
    }
}
