using AutoMapper;
using MediatR;
using OnShop.Domain.Notifications.Commands;
using OnShop.Domain.Notifications.Entities;
using OnShop.Domain.Notifications.Repositories;
using OnShop.Domain.SeedWork;
using OnShop.Framework.Dtos;
using OnShop.Framework.Resources.Interface;
using OnShop.Resources.Resources;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnShop.ApplicationServices.Notifications.Commands
{
    public class NotificationCommandHandler :
        IRequestHandler<NotificationCreateCommand, ResultDto>,
        IRequestHandler<NotificationDeleteCommand, ResultDto>,
        IRequestHandler<NotificationClearCommand, ResultDto>,
        IRequestHandler<NotificationMarkAsReadCommand, ResultDto>
    {
        private readonly INotificationRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IResourceManager _resourceManager;
        private readonly ResultDto _result;

        public NotificationCommandHandler(INotificationRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IResourceManager resourceManager)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _resourceManager = resourceManager;
            _result = new ResultDto(_resourceManager);
        }


        public async Task<ResultDto> Handle(NotificationDeleteCommand request, CancellationToken cancellationToken)
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
#if DEBUG
                _result.AddError(e.Message);
#else
                _result.AddError(SharedResource.SaveError);
#endif
            }
            return _result;

        }
        public async Task<ResultDto> Handle(NotificationClearCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var list = (await _repository.ListAllAsync()).Where(w => w.TargetUserId == request.TargetUserId);
                foreach (var item in list)
                {
                    if (item != null)
                    {
                        if (request.IsSoftDelete)
                            _repository.SoftDelete(item);
                        else
                            _repository.Delete(item);

                    }
                }
                _result.IsSuccess = await _unitOfWork.CommitAsync(cancellationToken) > 0;
            }
            catch (Exception e)
            {
                _result.Message = e.Message;
                _result.IsSuccess = false;
#if DEBUG
                _result.AddError(e.Message);
#else
                _result.AddError(SharedResource.SaveError);
#endif
            }
            return _result;

        }
        public async Task<ResultDto> Handle(NotificationMarkAsReadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _repository.GetByIdAsync(request.Id);
                if (res != null)
                {
                    res.IsRead = true;
                    res.ModifiedDate = DateTime.Now;
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
#if DEBUG
                _result.AddError(e.Message);
#else
                _result.AddError(SharedResource.SaveError);
#endif
            }
            return _result;
        }

        public async Task<ResultDto> Handle(NotificationCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var entity = new Notification()
                {
                    Link = request.Link,
                    Title = request.Title,
                    Type = request.Type,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    TargetUserId = request.TargetUserId

                };
                await _repository.AddAsync(entity);
                _result.IsSuccess = await _unitOfWork.CommitAsync(cancellationToken) > 0;

            }
            catch (Exception e)
            {
                _result.Message = e.Message;
#if DEBUG
                _result.AddError(e.Message);
#else
                _result.AddError(SharedResource.SaveError);
#endif

                _result.IsSuccess = false;
            }
            return _result;
        }
    }
}
