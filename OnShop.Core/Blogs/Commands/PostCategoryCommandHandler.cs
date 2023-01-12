using MediatR;
using OnShop.Domain.Blogs.Commands.PostCategories;
using OnShop.Domain.Blogs.Entities;
using OnShop.Domain.Blogs.Repositories;
using OnShop.Domain.SeedWork;
using OnShop.Framework.Dtos;
using OnShop.Framework.Resources.Interface;
using OnShop.Resources.Resources;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnShop.ApplicationServices.Blogs.Commands
{
    public class PostCategoryCommandHandler :
        IRequestHandler<PostCategoryCreateCommand, ResultDto>,
        IRequestHandler<PostCategoryDeleteCommand, ResultDto>,
        IRequestHandler<PostCategoryUpdateCommand, ResultDto>
    {
        private readonly IPostCategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResourceManager _resourceManager;
        private readonly ResultDto _result;

        public PostCategoryCommandHandler(IUnitOfWork unitOfWork, IResourceManager resourceManager, IPostCategoryRepository repository)
        {
            _unitOfWork = unitOfWork;
            _resourceManager = resourceManager;
            _result = new ResultDto(_resourceManager);
            _repository = repository;
        }

        public async Task<ResultDto> Handle(PostCategoryCreateCommand request, CancellationToken cancellationToken)
        {
            var entity = new PostCategory
            {
                CreateDate = DateTime.Now,
                CreatorUserId = request.CreatorUserId,
                Title = request.Title
            };
            await _repository.AddAsync(entity);
            _result.IsSuccess = await _unitOfWork.CommitAsync(cancellationToken) > 0;
            return _result;
        }

        public async Task<ResultDto> Handle(PostCategoryUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                if (entity != null)
                {
                    entity.Title = request.Title;
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedId = request.ModifierUserId;

                    _result.IsSuccess = await _unitOfWork.CommitAsync(cancellationToken) > 0;
                }
                else
                {
                    _result.Message = _resourceManager[SharedResource.NotFound];
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

        public async Task<ResultDto> Handle(PostCategoryDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var entity = await _repository.GetByIdAsync(request.Id);
                if (entity != null)
                {
                    entity.ModifiedId = request.ModifierUserId;
                    _repository.SoftDelete(entity);
                    _result.IsSuccess = await _unitOfWork.CommitAsync(cancellationToken) > 0;
                    _result.Message = _result.IsSuccess ? _resourceManager[SharedResource.DeleteMessage] : _resourceManager[SharedResource.SaveError];
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
                _result.Message = _resourceManager[SharedResource.SaveError];
                _result.IsSuccess = false;
                _result.AddError(e.Message);
            }
            return _result;
        }
    }
}
