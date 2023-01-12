using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnShop.Domain.SeedWork;
using OnShop.Domain.Stores.Commands;
using OnShop.Domain.Stores.Entities;
using OnShop.Domain.Stores.Repositories;
using OnShop.Framework.Dtos;
using OnShop.Framework.Resources.Interface;
using OnShop.Resources.Resources;

namespace OnShop.ApplicationServices.Stores.Command
{
    public class StoreCommandHandler :
        IRequestHandler<StoreCreateCommand, ResultDto>,
        IRequestHandler<StoreUpdateCommand, ResultDto>,
        IRequestHandler<StoreDeleteCommand, ResultDto>
    {
        private readonly IStoreRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IResourceManager _resourceManager;
        private readonly ResultDto _result;

        public StoreCommandHandler(IStoreRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IResourceManager resourceManager)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _resourceManager = resourceManager;
            _result = new ResultDto(_resourceManager);

        }

        public async Task<ResultDto> Handle(StoreCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (IsValid(request))
                {
                    var mapper = new Store
                    {
                        CreateDate = DateTime.Now,
                        CreatorUserId = request.CreatorUserId,
                        Description = request.Description,
                        Title = request.Title,
                        StoreType = request.StoreType,
                        MembershipType = request.MembershipType,
                        PhoneNumber = request.PhoneNumber,
                        Address = request.Address,
                        BigLogo = request.BigLogo,
                        SmallLogo = request.SmallLogo,
                        IsRemoved = false,
                        //Code = 
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

        public async Task<ResultDto> Handle(StoreUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _repository.GetByIdAsync(request.Id);
                if (res != null)
                {
                    res.Description = request.Description;
                    res.StoreType = request.StoreType;
                    res.MembershipType = request.MembershipType;
                    res.PhoneNumber = request.PhoneNumber;
                    res.Title = request.Title;
                    res.Address = request.Address;
                    res.Code = request.Code;
                    res.BigLogo = !string.IsNullOrEmpty(request.BigLogo) ? request.BigLogo : res.BigLogo;
                    res.SmallLogo = !string.IsNullOrEmpty(request.SmallLogo) ? request.SmallLogo : res.SmallLogo; 
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

        public async Task<ResultDto> Handle(StoreDeleteCommand request, CancellationToken cancellationToken)
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

        private bool IsValid(StoreCreateCommand request)
        {
            return true;
        }
    }
}