using MediatR;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Notifications.Queries
{
    public class NotificationUnReadCountQueries : IRequest<ResultDto<int>>
    {
    }
    
    
}