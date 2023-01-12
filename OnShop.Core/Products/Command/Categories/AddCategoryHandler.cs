using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnShop.ApplicationServices.Specifications.Products;
using OnShop.Domain.Product.Commands.Categories;
using OnShop.Domain.Product.Entities;
using OnShop.Domain.Product.Repositories.Categories;
using OnShop.Domain.SeedWork;
using OnShop.Framework.Dtos;
using OnShop.Framework.Resources.Interface;
using OnShop.Resources.Resources;

namespace OnShop.ApplicationServices.Products.Command.Categories
{
    public class AddCategoryHandler :
        IRequestHandler<CategoryCreateCommand, ResultDto>,
        IRequestHandler<DeleteCategoryByIdCommand, ResultDto>,
        IRequestHandler<CategoryUpdateCommand, ResultDto>
    {
        private readonly ICategoryCommandRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IResourceManager _resourceManager;
        private readonly ResultDto _result;

        public AddCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper, IResourceManager resourceManager, ICategoryCommandRepository repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _resourceManager = resourceManager;
            _result = new ResultDto(_resourceManager);
            _repository = repository;
        }

        public async Task<ResultDto> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
        {
            var mapper = new Category
            {
                CreateDate = request.CreateDate,
                CreatorUserId = request.CreatorUserId,
                ParentId = request.ParentId,
                Title = request.Title,
                Icon = request.Icon,
            };
            await _repository.AddAsync(mapper);
            _result.IsSuccess = await _unitOfWork.CommitAsync(cancellationToken) > 0;
            return _result;
        }

        public async Task<ResultDto> Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var brand = await _repository.GetByIdAsync(request.Id);
                if (brand != null)
                {
                    _repository.Delete(brand);
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
                _result.Message = _resourceManager[SharedResource.SaveError];
                _result.IsSuccess = false;
                _result.AddError(e.Message);
            }
            return _result;
        }

        public async Task<ResultDto> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _repository.GetByIdAsync(request.Id);
                if (data != null)
                {
                    data.ParentId = request.ParentId;
                    data.Title = request.Title;
                    if (!string.IsNullOrEmpty(request.Icon) && request.Icon.Length > 2)
                    {
                        data.Icon = request.Icon;
                    }
                    data.ModifiedDate = DateTime.Now;
                    data.ModifiedId = request.ModifiedId;

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
    }
}
