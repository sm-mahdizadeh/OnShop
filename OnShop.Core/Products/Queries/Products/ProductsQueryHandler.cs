using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnShop.ApplicationServices.Specifications.Products;
using OnShop.Domain.DTOs.Site.Products;
using OnShop.Domain.Product.Dtos.Product;
using OnShop.Domain.Product.Queries.Products;
using OnShop.Domain.Product.Repositories.Products;
using OnShop.Domain.SeedWork;
using OnShop.Framework.Resources.Interface;
using OnShop.Resources.Resources;
using ProductFeaturesSiteDto = OnShop.Domain.DTOs.Site.Products.ProductFeaturesSiteDto;

namespace OnShop.ApplicationServices.Products.Queries.Products
{
    public class ProductsQueryHandler :
        IRequestHandler<GetProductQueries, ResultProductSiteDto>,
        IRequestHandler<GetProductByIdQueries, ResultProductDetailsSiteDto>,
        IRequestHandler<GetAdminProductByIdQueries, ProductDto>
    {
        private readonly IProductsRepository _repository;
        private readonly IMapper _mapper;
        private readonly IResourceManager _resourceManager;
        private readonly IUnitOfWork _unitOfWork;
        public ProductsQueryHandler(IProductsRepository repository, IMapper mapper, IResourceManager resourceManager, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _resourceManager = resourceManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultProductSiteDto> Handle(GetProductQueries request, CancellationToken cancellationToken)
        {
            //var spec = new ProductSpecification(request.SearchKey, request.SortColumn, categoryId: request.CategoryId);
            var spec = new ProductSpecification(request.SearchKey, request.SortColumn, categoryId: request.CategoryId, order: request.Ordering);
            var result = new ResultProductSiteDto
            {
                TotalRow = await _repository.CountAsync(spec)
            };
            var res = await _repository.GetPagedRespondAsync(spec, request.PageSize, request.Skip);
            result.Products = res.Select(x => new ProductSiteDto
            {

                Title = x.Title,
                EnglishTitle = x.EnglishTitle,
                Id = x.Id,
                ImageSrc = x.ProductImages?.Select(y => y.Src).ToList(),
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                Price = CurrentPrice(x.Price, x.Discount, x.PriceDiscount),
                OldPrice = x.Price,
                Quantity = x.Quantity,
                CurrentPrice = CurrentPrice(x.Price, x.Discount, x.PriceDiscount) > 0 ? CurrentPrice(x.Price, x.Discount, x.PriceDiscount) : x.Price,
                CreatorUserId = x.CreatorUserId,
                Code = x.Code,
                Star = 5,
                CreateDate = x.CreateDate,
                CategoryName = x.Category.Title,
                CategoryId = x.CategoryId,
                BrandId = x.BrandId,
                Discount = CalculatePercentage(x.Price, x.Discount, x.PriceDiscount),
                Available = x.Quantity > 0 && x.CanPurchase ? _resourceManager[SharedResource.Available] : _resourceManager[SharedResource.Unavailable],
                Tag = x.Tag,
                CanPurchase = x.CanPurchase,
                BrandName = x.Brand.Title,

            }).ToList();
            return result;
        }

        private decimal CurrentPrice(decimal price, int? discount = 0, decimal? priceDiscount = 0)
        {
            var newPrice = 0M;
            if (discount > 0)
            {
                newPrice = price - (price * discount.GetValueOrDefault() / 100);
            }
            else if (priceDiscount > 0)
            {
                newPrice = price - priceDiscount.GetValueOrDefault();
            }
            return newPrice;
        }

        private int CalculatePercentage(decimal price, int? discount = 0, decimal? priceDiscount = 0)
        {
            var percentage = 0;
            if (discount > 0)
            {
                percentage = discount.GetValueOrDefault();
            }
            else if (priceDiscount > 0)
            {
                percentage = Convert.ToInt32(Math.Round(priceDiscount.GetValueOrDefault() * 100) / price);
            }
            return percentage;
        }

        public async Task<ResultProductDetailsSiteDto> Handle(GetProductByIdQueries request, CancellationToken cancellationToken)
        {
            var model = new ResultProductDetailsSiteDto();
            var result = await _repository.FirstOrDefaultAsync(new ProductSpecification(request.Id));
            if (result != null)
            {
                var currentPrice = CurrentPrice(result.Price, result.Discount, result.PriceDiscount);
                result.ViewCount++;
                await _unitOfWork.CommitAsync(cancellationToken);
                model.Product = new ProductSiteDto
                {
                    Title = result.Title,
                    EnglishTitle = result.EnglishTitle,
                    Id = result.Id,
                    ImageSrc = result.ProductImages?.Select(y => y.Src).ToList(),
                    ShortDescription = result.ShortDescription,
                    Description = result.Description,

                    Price = currentPrice,
                    OldPrice = result.Price,

                    Quantity = result.Quantity,
                    CurrentPrice = currentPrice > 0 ? currentPrice : result.Price,
                    CreatorUserId = result.CreatorUserId,
                    Code = result.Code,
                    Star = 5,
                    CreateDate = result.CreateDate,
                    CategoryName = result.Category?.Title,
                    CategoryId = result.CategoryId,
                    BrandId = result.BrandId,
                    Discount = CalculatePercentage(result.Price, result.Discount, result.PriceDiscount),
                    Available = result.Quantity > 0 && result.CanPurchase ? _resourceManager[SharedResource.Available] : _resourceManager[SharedResource.Unavailable],
                    CanPurchase = result.CanPurchase,
                    Tag = result.Tag
                };
                model.ProductFeatures = result.ProductFeatures.Where(x => !x.IsRemoved).Select(x => new ProductFeaturesSiteDto
                {
                    Id = x.Id,
                    CreateDate = x.CreateDate,
                    CreatorUserId = x.CreatorUserId,
                    EnglishTitle = x.EnglishTitle,
                    Title = x.Title,
                    Description = x.Description,

                }).ToList();
                model.ProductTechnical = result.ProductTechnicals.Where(x => !x.IsRemoved).Select(x =>
                    new ResultProductTechnicalSiteDto
                    {
                        Id = x.Id,
                        CreateDate = x.CreateDate,
                        CreatorUserId = x.CreatorUserId,
                        Title = x.Title,
                        Description = x.Description,
                    }).ToList();
                model.ProductAddToCart = new ProductAddToCart()
                {
                    ProductId = request.Id,
                    Count = 1
                };
            }

            return model;
        }

        public async Task<ProductDto> Handle(GetAdminProductByIdQueries request, CancellationToken cancellationToken)
        {

            var result = await _repository.FirstOrDefaultAsync(new ProductSpecification(request));
            if (result != null)
            {
                var model = new ProductDto()
                {
                    Title = result.Title,
                    EnglishTitle = result.EnglishTitle,
                    Id = result.Id,
                    ShortDescription = result.ShortDescription,
                    Description = result.Description,

                    Price = result.Price,
                    PriceDiscount = result.PriceDiscount,
                    Discount = result.Discount,
                    
                    Quantity = result.Quantity,
                    Code = result.Code,
                    
                    CategoryId = result.CategoryId,
                    BrandId = result.BrandId,
                    Tag = result.Tag,
                    CanPurchase = result.CanPurchase,
                    Displayed = result.Displayed,
                    
                };
                return model;

            }

            return null;
        }
    }
}