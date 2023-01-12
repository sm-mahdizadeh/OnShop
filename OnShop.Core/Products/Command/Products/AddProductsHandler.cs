using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnShop.Domain.Product.Commands.Brands;
using OnShop.Domain.Product.Commands.Products;
using OnShop.Domain.Product.Entities;
using OnShop.Domain.Product.Repositories.Categories;
using OnShop.Domain.Product.Repositories.Products;
using OnShop.Domain.SeedWork;
using OnShop.Framework.Dtos;
using OnShop.Framework.Resources.Interface;
using OnShop.Resources.Resources;

namespace OnShop.ApplicationServices.Products.Command.Products
{
    public class AddProductsHandler :
                 IRequestHandler<ProductsCreateCommand, ResultDto>,
                 IRequestHandler<DeleteProductCommand, ResultDto>,
                 IRequestHandler<ProductsUpdateCommand, ResultDto>
    {
        private readonly IProductsRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IResourceManager _resourceManager;
        private readonly ResultDto _result;

        public AddProductsHandler(IProductsRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IResourceManager resourceManager)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _resourceManager = resourceManager;
            _result = new ResultDto(_resourceManager);

        }

        public async Task<ResultDto> Handle(ProductsCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request != null)
                {
                    if (await IsValid(request))
                    {

                        var mapper = new Product
                        {
                            CreateDate = DateTime.Now,
                            CreatorUserId = request.CreatorUserId,

                            Description = request.Description,
                            Title = request.Title,
                            BrandId = request.BrandId,
                            CategoryId = request.CategoryId,

                            Displayed = request.Displayed,
                            CanPurchase = request.CanPurchase,
                            Code = request.Code,

                            Discount = request.Discount,
                            PriceDiscount = request.PriceDiscount ?? 0,
                            Price = request.Price,

                            Color = request.Color,
                            EnglishTitle = request.EnglishTitle,
                            Quantity = request.Quantity,
                            Tag = request.Tag,
                            ViewCount = 0,
                            ShortDescription = request.ShortDescription,
                            ProductImages = ProductImageMapper(request.ProductImages),
                            ProductTechnicals = ProductTechnicalMapper(request.ProductTechnician, request.CreatorUserId.GetValueOrDefault()),
                            ProductFeatures = ProductFeaturesMapper(request.ProductFeatures, request.CreatorUserId.GetValueOrDefault()),
                        };

                        await _repository.AddAsync(mapper);
                        _result.IsSuccess = await _unitOfWork.CommitAsync(cancellationToken) > 0;

                    }
                }
            }
            catch (Exception e)
            {
                _result.Message = e.Message;
                _result.AddError(SharedResource.SaveError);
                _result.IsSuccess = false;
            }

            return _result;
        }

        private async Task<bool> IsValid(ProductsCreateCommand model)
        {
            if (!model.ProductImages.Any())
            {
                _result.AddError(SharedResource.ProductImagesError);
                _result.IsSuccess = false;
            }
            if (!model.ProductFeatures.Any())
            {
                _result.AddError(SharedResource.ProductFeaturesError);
                _result.IsSuccess = false;
            }
            if (!model.ProductTechnician.Any())
            {
                _result.AddError(SharedResource.ProductTechnicianError);
                _result.IsSuccess = false;
            }
            if (!await _repository.IsUniqueProductCodeAsync(model.Code))
            {
                _result.AddError(SharedResource.IsNotUnique, _resourceManager[SharedResource.Code]);
                _result.IsSuccess = false;
            }
            return _result.IsSuccess;
        }

        #region Mapper

        private List<ProductFeature> ProductFeaturesMapper(List<ProductFeaturesCommand> features, int creatorUserId)
        {
            var productFeatures = new List<ProductFeature>();
            foreach (var feature in features)
            {
                productFeatures.Add(new ProductFeature()
                {
                    CreateDate = DateTime.Now,
                    CreatorUserId = creatorUserId,
                    Description = feature.Description,
                    EnglishTitle = feature.EnglishTitle,
                    Title = feature.Title,

                });
            }
            return productFeatures;
        }
        private List<ProductImage> ProductImageMapper(List<ProductImagesCommand> images)
        {
            var productImages = new List<ProductImage>();
            foreach (var image in images)
            {
                productImages.Add(new ProductImage()
                {
                    IsShow = image.IsShow,
                    IsBaseImage = image.IsBaseImage,
                    Src = image.Src
                });
            }
            return productImages;
        }

        private List<ProductTechnical> ProductTechnicalMapper(List<ProductTechnicalCommand> technicals, int creatorUserId)
        {
            var productTechnical = new List<ProductTechnical>();
            foreach (var technical in technicals)
            {
                productTechnical.Add(new ProductTechnical()
                {
                    CreateDate = DateTime.Now,
                    CreatorUserId = creatorUserId,

                    Description = technical.Description,
                    Title = technical.Title

                });
            }
            return productTechnical;
        }

        #endregion

        public async Task<ResultDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var res = await _repository.GetByIdAsync(request.Id);
                if (res != null)
                {
                    _repository.SoftDelete(res);
                    _result.IsSuccess = await _unitOfWork.CommitAsync(cancellationToken) > 0;
                    if (_result.IsSuccess)
                        _result.Message = _resourceManager[SharedResource.DeleteMessage];
                }
                else
                {
                    _result.IsSuccess = false;
                    _result.AddError(SharedResource.NotFound);
                    _result.Message = _resourceManager[SharedResource.NotFound];
                }
            }
            catch (Exception e)
            {
                _result.Message = e.Message;
                _result.IsSuccess = false;
                _result.AddError(SharedResource.SaveError);
            }
            return _result;
        }

        public async Task<ResultDto> Handle(ProductsUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _repository.GetByIdAsync(request.Id);
                if (product != null)
                {
                    product.ModifiedId = request.ModifiedId;
                    product.ModifiedDate = DateTime.Now;
                    product.Description = request.Description;
                    product.Title = request.Title;
                    product.BrandId = request.BrandId;
                    product.CategoryId = request.CategoryId;

                    product.Displayed = request.Displayed;
                    product.CanPurchase = request.CanPurchase;
                    //product.Code = request.Code;

                    product.Discount = request.Discount;
                    product.PriceDiscount = request.PriceDiscount ?? 0;
                    product.Price = request.Price;

                    //product.Color = request.Color;
                    product.EnglishTitle = request.EnglishTitle;
                    product.Quantity = request.Quantity;
                    product.Tag = request.Tag;
                    product.ShortDescription = request.ShortDescription;
                    _result.IsSuccess = await _unitOfWork.CommitAsync(cancellationToken) > 0;
                }
                else
                {
                    _result.IsSuccess = false;
                    _result.AddError(SharedResource.NotFound);
                    _result.Message = _resourceManager[SharedResource.NotFound];
                }
            }
            catch (Exception e)
            {
                _result.Message = e.Message;
                _result.IsSuccess = false;
                _result.AddError(SharedResource.SaveError);
            }
            return _result;

        }
    }
}