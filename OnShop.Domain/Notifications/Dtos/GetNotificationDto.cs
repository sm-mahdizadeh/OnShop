using OnShop.Domain.DTOs;
using OnShop.Domain.Enum;

namespace OnShop.Domain.Notifications.Dtos
{
    public class GetNotificationDto : BaseModelDto<long>
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; }
        public int TargetUserId { get; set; }
    }
}
