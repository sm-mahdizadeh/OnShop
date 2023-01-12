using MediatR;
using OnShop.Domain.DTOs;
using OnShop.Domain.DTOs.Site.Products;
using OnShop.Domain.Product.Dtos.Product;

namespace OnShop.Domain.Product.Queries.Products
{
    public class GetProductQueries : BaseQueries, IRequest<ResultProductSiteDto>
    {
        public long? CategoryId { get; set; }
    }
    public class GetProductByIdQueries : IRequest<ResultProductDetailsSiteDto>
    {
        public long Id { get; set; }
    }

    public class GetAdminProductByIdQueries : IRequest<ProductDto>
    {
        public long Id { get; set; }
    }

    public class GetProductFilterQueries : IRequest<ProductFilters>
    {
        
    }



}