using MediatR;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Blogs.Commands.PostCategories
{
    public class PostCategoryDeleteCommand : IRequest<ResultDto>
    {
        public int Id { get; set; }
        public int ModifierUserId { get; set; }
    }
}