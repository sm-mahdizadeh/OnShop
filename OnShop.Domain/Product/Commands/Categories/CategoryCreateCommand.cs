using MediatR;
using OnShop.Framework.Commands;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Product.Commands.Categories
{
    public class CategoryCreateCommand : BaseCommandEntity, IRequest<ResultDto>
    {
        public string Title { get; set; }
        public long? ParentId { get; set; }
        public string Icon { get; set; }
    }
    public class CategoryUpdateCommand : CategoryCreateCommand
    {
        public long Id { get; set; }
    }
}