using System.Collections.Generic;
using MediatR;
using OnShop.Domain.Product.Commands.Brands;
using OnShop.Framework.Commands;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Product.Commands.Products
{
    public class ProductsCreateCommand : BaseCommandEntity, IRequest<ResultDto>
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string EnglishTitle { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Tag { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public decimal? PriceDiscount { get; set; }
        public int? Discount { get; set; }
        public bool Displayed { get; set; }
        public bool CanPurchase { get; set; }
        public int Quantity { get; set; }
        public int BrandId { get; set; }
        public long CategoryId { get; set; }

        public List<ProductImagesCommand> ProductImages { get; set; }
        public List<ProductFeaturesCommand> ProductFeatures { get; set; }
        public List<ProductTechnicalCommand> ProductTechnician { get; set; }
    }
    
    public  class ProductsUpdateCommand : ProductsCreateCommand
    {
        public long Id { get; set; }
    }
}