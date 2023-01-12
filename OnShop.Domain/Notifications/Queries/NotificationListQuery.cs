using MediatR;
using OnShop.Domain.DTOs;
using OnShop.Domain.Notifications.Dtos;

namespace OnShop.Domain.Notifications.Queries
{
    public class NotificationListQuery : BaseQueries, IRequest<QueryList<GetNotificationDto>>
    {
    }
    
    
}