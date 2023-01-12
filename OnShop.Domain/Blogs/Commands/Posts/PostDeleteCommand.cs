using MediatR;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Blogs.Commands.Posts
{
    public class PostDeleteCommand : IRequest<ResultDto>
    {
        public long Id { get; set; }
        public bool IsSoftDelete { get; set; } = false;
        public int ModifierUserId { get; set; }
    }
}