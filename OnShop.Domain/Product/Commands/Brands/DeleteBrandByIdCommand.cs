using MediatR;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Product.Commands.Brands
{
    public class DeleteBrandByIdCommand : IRequest<ResultDto>
    {
        public int Id { get; set; }
    }
}