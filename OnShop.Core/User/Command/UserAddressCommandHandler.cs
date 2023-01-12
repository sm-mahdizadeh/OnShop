using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnShop.Domain.SeedWork;
using OnShop.Domain.User.Commands;
using OnShop.Domain.User.Entities;
using OnShop.Domain.User.Repositories;
using OnShop.Framework.Dtos;
using OnShop.Framework.Resources.Interface;
using OnShop.Resources.Resources;

namespace OnShop.ApplicationServices.User.Command
{
    public class UserAddressCommandHandler :
        IRequestHandler<AddUserAddressCommand, ResultDto>,
        IRequestHandler<DeleteUserAddressCommand, ResultDto>
    {
        private readonly IUserAddressRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IResourceManager _resourceManager;
        private readonly ResultDto _result;

        public UserAddressCommandHandler(IUserAddressRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IResourceManager resourceManager)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _resourceManager = resourceManager;
            _result = new ResultDto(_resourceManager);

        }

        public async Task<ResultDto> Handle(AddUserAddressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (IsValid(request))
                {
                    var mapper = Mapper(request);
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

        private UserAddress Mapper(AddUserAddressCommand command)
        {
            return new UserAddress()
            {
                CreatorUserId = command.CreatorUserId,
                CreateDate = DateTime.Now,
                IsRemoved = false,
                ApplicationUserId = command.ApplicationUserId,
                DistrictId = command.DistrictId,
                Plaque = command.Plaque,
                PostCode = command.PostCode,
                PostalAddress = command.PostalAddress,
                Unit = command.Unit,
                RecipientFirstName = "me",
                RecipientLastName = "me"
            };
        }

        private bool IsValid(AddUserAddressCommand req)
        {
            return true;
        }

        public async Task<ResultDto> Handle(DeleteUserAddressCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var res = await _repository.GetByIdAsync(request.Id);
                if (res != null)
                {
                    _repository.SoftDelete(res);
                    _result.IsSuccess = await _unitOfWork.CommitAsync(cancellationToken) > 0;
                    if (_result.IsSuccess)
                        _result.Message = "موفقیت آمیز بود";
                }
                else
                {
                    _result.IsSuccess = false;
                    _result.Message =_resourceManager[SharedResource.NotFound];
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