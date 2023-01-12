using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnShop.Domain.SeedWork;
using OnShop.Domain.Slider.Commands;
using OnShop.Domain.Slider.Repositories;
using OnShop.Framework.Dtos;
using OnShop.Framework.Resources.Interface;
using OnShop.Resources.Resources;

namespace OnShop.ApplicationServices.Slider.Command
{
    public class SliderCommandHandler :
        IRequestHandler<AddSliderCommand, ResultDto>,
        IRequestHandler<UpdateSliderCommand, ResultDto>,
        IRequestHandler<DeleteSliderCommand, ResultDto>
    {
        private readonly ISliderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IResourceManager _resourceManager;
        private readonly ResultDto _result;

        public SliderCommandHandler(ISliderRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IResourceManager resourceManager)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _resourceManager = resourceManager;
            _result = new ResultDto(_resourceManager);
        }

        public async Task<ResultDto> Handle(AddSliderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (IsValid(request))
                {
                    var mapper = new Domain.Slider.Entities.Slider()
                    {
                        CreateDate = DateTime.Now,
                        Description = request.Description,
                        Link = request.Link,
                        LinkTitle = request.LinkTitle,
                        Src = request.Src,
                        Title = request.Title,
                        CreatorUserId = request.CreatorUserId,
                        IsRemoved = false,
                    };
                    await _repository.AddAsync(mapper);
                    _result.IsSuccess = await _unitOfWork.CommitAsync(cancellationToken) > 0;
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


        public async Task<ResultDto> Handle(UpdateSliderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _repository.GetByIdAsync(request.Id);
                if (res != null)
                {
                    res.Description = request.Description;
                    res.Link = request.Link;
                    res.LinkTitle = request.LinkTitle;
                    res.Src = string.IsNullOrEmpty(request.Src) ? res.Src : request.Src;
                    res.Title = request.Title;
                    res.ModifiedDate = DateTime.Now;
                    res.ModifiedId = request.ModifiedId;
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

        public async Task<ResultDto> Handle(DeleteSliderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _repository.GetByIdAsync(request.Id);
                if (res != null)
                {
                    if (request.IsSoftDelete)
                        _repository.SoftDelete(res);
                    else
                        _repository.Delete(res);
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

        private bool IsValid(AddSliderCommand request)
        {
            if (string.IsNullOrEmpty(request.Src))
            {
                _result.AddError(SharedResource.ValidChecker, _resourceManager[SharedResource.Src]);
                _result.IsSuccess = false;
            }
            if (string.IsNullOrEmpty(request.Title))
            {
                _result.AddError(SharedResource.ValidChecker, _resourceManager[SharedResource.Title]);
                _result.IsSuccess = false;
            }
            return _result.IsSuccess;
        }

    }
}