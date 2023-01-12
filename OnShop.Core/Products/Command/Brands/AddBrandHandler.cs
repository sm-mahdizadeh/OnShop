using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnShop.Domain.Product.Commands.Brands;
using OnShop.Domain.Product.Entities;
using OnShop.Domain.Product.Repositories.Brands;
using OnShop.Domain.SeedWork;
using OnShop.Framework.Dtos;
using OnShop.Framework.Resources.Interface;
using OnShop.Resources.Resources;

namespace OnShop.ApplicationServices.Products.Command.Brands
{
    public class AddBrandHandler : 
        IRequestHandler<BrandCreateCommand, ResultDto>,
        IRequestHandler<BrandUpdateCommand, ResultDto>,
        IRequestHandler<DeleteBrandByIdCommand, ResultDto>
    {

        private readonly IBrandsCommandRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IResourceManager _resourceManager;
        private readonly ResultDto _result;

        public AddBrandHandler(IBrandsCommandRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IResourceManager resourceManager)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _resourceManager = resourceManager;
            _result = new ResultDto(_resourceManager);
        }

        public async Task<ResultDto> Handle(BrandCreateCommand request, CancellationToken cancellationToken)
        {
            if (await IsValid(request))
            {
                var mapper = new Brand
                {
                    Description = request.Description,
                    EnglishTitle = request.EnglishTitle,
                    Remarks = request.Remarks,
                    Src = request.Src,
                    Title = request.Title
                };
                await _repository.AddAsync(mapper);
                _result.IsSuccess = await _unitOfWork.CommitAsync(cancellationToken) > 0;
            }

            return _result;
        }

        private async Task<bool> IsValid(BrandCreateCommand request, int? brandId = null)
        {
            var isUnique = await _repository.IsUniqueBrandAsync(request.Title, brandId);

            if (isUnique)
            {
                _result.AddError(SharedResource.IsNotUnique, _resourceManager[SharedResource.Title]);
                _result.IsSuccess = false;
            }

            return _result.IsSuccess;
        }


        public async Task<ResultDto> Handle(DeleteBrandByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var res = await _repository.GetByIdAsync(request.Id);
                if (res != null)
                {
                    _repository.Delete(res);
                    _result.IsSuccess = await _unitOfWork.CommitAsync(cancellationToken) > 0;
                }
                else
                {
                    _result.IsSuccess = false;
                    _result.AddError(SharedResource.NotFound);
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

        public async Task<ResultDto> Handle(BrandUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await IsValid(request, request.Id))
                {
                    var brand = await _repository.GetByIdAsync(request.Id);
                    if (brand != null)
                    {

                        brand.Src = request.Src;
                        brand.Description = request.Description;
                        brand.EnglishTitle = request.EnglishTitle;
                        brand.Title = request.Title;
                        brand.Remarks = request.Remarks;
                        _result.IsSuccess = await _unitOfWork.CommitAsync(cancellationToken) > 0;
                    }
                    else
                    {
                        _result.IsSuccess = false;
                        _result.AddError(SharedResource.NotFound);
                        _result.Message = _resourceManager[SharedResource.NotFound];
                    }
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