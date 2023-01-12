using MediatR;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Product.Commands.Categories
{
    public class DeleteCategoryByIdCommand : IRequest<ResultDto>
    {
        public long Id { get; set; }
    }
}