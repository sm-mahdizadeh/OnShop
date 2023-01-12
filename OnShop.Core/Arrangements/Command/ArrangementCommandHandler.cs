using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnShop.Domain.Arrangements.Commands;
using OnShop.Domain.Arrangements.Entities;
using OnShop.Domain.Arrangements.Repositories;
using OnShop.Domain.SeedWork;
using OnShop.Framework.Dtos;
using OnShop.Framework.Resources.Interface;
using OnShop.Resources.Resources;

namespace OnShop.ApplicationServices.Arrangements.Command
{
    public class ArrangementCommandHandler :
        IRequestHandler<AddArrangementCommand, ResultDto>,
        IRequestHandler<DeleteArrangementCommand, ResultDto>,
        IRequestHandler<UpdateArrangementCommand, ResultDto>

    {
        private readonly IArrangementRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IResourceManager _resourceManager;
        private readonly ResultDto _result;

        public ArrangementCommandHandler(IArrangementRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IResourceManager resourceManager)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _resourceManager = resourceManager;
            _result = new ResultDto(_resourceManager);
        }

        public async Task<ResultDto> Handle(AddArrangementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (IsValid())
                {
                    var mapper = new Arrangement()
                    {
                        StoreId = request.StoreId,
                        Type = request.Type,
                        Priority = request.Priority,
                        TargetId = request.TargetId,
                        Description = request.Description,
                        CreateDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        
                    };
                    await _repository.AddAsync(mapper);
                    _result.IsSuccess = await _unitOfWork.CommitAsync(cancellationToken) > 0;
                }
                else
                {
                    _result.IsSuccess = false;
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

        private bool IsValid()
        {
            return true;
        }

        public async Task<ResultDto> Handle(DeleteArrangementCommand request, CancellationToken cancellationToken)
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

        public async Task<ResultDto> Handle(UpdateArrangementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _repository.GetByIdAsync(request.Id);
                if (res != null)
                {
                    res.Description = request.Description;
                    res.Type = request.Type;
                    res.Priority = request.Priority;
                    res.TargetId = request.TargetId;
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
    }
}
