using AutoMapper;
using MediatR;
using OnShop.Domain.Notifications.Queries;
using OnShop.Domain.Notifications.Repositories;
using OnShop.Framework.Dtos;
using OnShop.Framework.Resources.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OnShop.ApplicationServices.Specifications.Notification;
using OnShop.Domain.DTOs;
using OnShop.Domain.Notifications.Dtos;
using OnShop.Domain.Notifications.Entities;
using OnShop.Resources.Resources;

namespace OnShop.ApplicationServices.Notifications.Queries
{
    public class NotificationQueryHandler :
        IRequestHandler<NotificationListQuery, QueryList<GetNotificationDto>>,
        IRequestHandler<NotificationQueries, IReadOnlyList<GetNotificationDto>>,
        IRequestHandler<NotificationByIdQueries, ResultDto<GetNotificationDto>>,
        IRequestHandler<NotificationUnReadCountQueries, ResultDto<int>>
    {
        private readonly INotificationRepository _repository;
        private readonly ResultDto<GetNotificationDto> _result;
        private readonly IMapper _mapper;
        private readonly IResourceManager _resourceManager;

        public NotificationQueryHandler(INotificationRepository repository, IMapper mapper, IResourceManager resourceManager)
        {
            _repository = repository;
            _mapper = mapper;
            _resourceManager = resourceManager;
            _result = new ResultDto<GetNotificationDto>(_resourceManager);
        }

        private GetNotificationDto Mapper(Notification entity)
        {
            return new GetNotificationDto()
            {
                Id = entity.Id,
                CreatorUserId = entity.CreatorUserId,
                CreateDate = entity.CreateDate,
                ModifiedDate = entity.ModifiedDate,
                ModifiedId = entity.ModifiedId,
                RemoveTime = entity.RemoveTime,
                IsRemoved = entity.IsRemoved,

                IsRead = entity.IsRead,
                Link = entity.Link,
                TargetUserId = entity.TargetUserId,
                Title = entity.Title,
                Type = entity.Type

            };
        }

        public async Task<QueryList<GetNotificationDto>> Handle(NotificationListQuery request, CancellationToken cancellationToken)
        {
            var spec = new NotificationSpecification();
            var count = await _repository.CountAsync(spec);
            var lst = await _repository.GetPagedRespondAsync(spec, request.PageSize, request.Skip);
            return new QueryList<GetNotificationDto> { Data = lst.Select(Mapper).OrderBy(o=>o.IsRead).ThenByDescending(d=>d.CreateDate).ToList(), TotalCount = count };
        }

        public async Task<IReadOnlyList<GetNotificationDto>> Handle(NotificationQueries request, CancellationToken cancellationToken)
        {
            var lst = await _repository.ListAllAsync();
            return lst?.Select(Mapper).ToList().AsReadOnly();
        }

        public async Task<ResultDto<GetNotificationDto>> Handle(NotificationByIdQueries request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(request.Id);

            if (result != null)
            {
                _result.Data = Mapper(result);

            }
            else
            {
                _result.IsSuccess = false;
                _result.AddError(SharedResource.NotFound);
            }
            return _result;
        }

        public async Task<ResultDto<int>> Handle(NotificationUnReadCountQueries request, CancellationToken cancellationToken)
        {
            var res = new ResultDto<int>(_resourceManager);
            try
            {
                var cartItemCount = await _repository.CountAsync(new NotificationSpecification(false));
                res.Data = cartItemCount;

            }
            catch (Exception)
            {
                res.Data = 0;
            }
            return res;
        }
    }
}
