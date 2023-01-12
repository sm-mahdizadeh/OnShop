using MediatR;
using OnShop.Domain.Notifications.Dtos;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Notifications.Queries
{
    public class NotificationByIdQueries : IRequest<ResultDto<GetNotificationDto>>
    {
        public long Id { get; set; }
    }
    
    
}