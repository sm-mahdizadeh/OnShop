using MediatR;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Product.Commands.Products
{
    public class DeleteProductCommand : IRequest<ResultDto>
    {
        public long Id { get; set; }
    }
}