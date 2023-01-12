using System.Collections.Generic;
using MediatR;
using OnShop.Domain.Notifications.Dtos;

namespace OnShop.Domain.Notifications.Queries
{
    public class NotificationQueries : IRequest<IReadOnlyList<GetNotificationDto>>
    {
        
    }   
}