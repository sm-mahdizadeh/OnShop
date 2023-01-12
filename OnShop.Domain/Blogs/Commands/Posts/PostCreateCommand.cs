using MediatR;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Blogs.Commands.Posts
{
    public class PostCreateCommand :  IRequest<ResultDto>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public string Image { get; set; }
        public int? CategoryId { get; set; }
        public bool IsActive { get; set; }
        public int CreatorUserId { get; set; }
    }
}