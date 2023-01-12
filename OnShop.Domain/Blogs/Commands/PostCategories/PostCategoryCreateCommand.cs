using MediatR;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Blogs.Commands.PostCategories
{
    public class PostCategoryCreateCommand :  IRequest<ResultDto>
    {
        public string Title { get; set; }
        public int CreatorUserId { get; set; }
    }
}