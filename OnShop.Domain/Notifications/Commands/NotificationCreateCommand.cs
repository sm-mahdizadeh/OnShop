using MediatR;
using OnShop.Domain.Enum;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Notifications.Commands
{
    public class NotificationCreateCommand : IRequest<ResultDto>
    {
        public int TargetUserId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public NotificationType Type { get; set; }
    }
}
