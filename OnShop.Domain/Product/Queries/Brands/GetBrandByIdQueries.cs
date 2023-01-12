using MediatR;
using OnShop.Domain.Product.Dtos.Brands;

namespace OnShop.Domain.Product.Queries.Brands
{
    public class GetBrandByIdQueries : IRequest<GetBrandDto>
    {
        public int Id { get; set; }
    }
}