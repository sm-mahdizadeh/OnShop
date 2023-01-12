using OnShop.Domain.Common;
using OnShop.Domain.Enum;
using OnShop.Domain.Interfaces;
using OnShop.Domain.User.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnShop.Domain.Notifications.Entities
{
    public class Notification : BaseUserEntity<long>, IAggregateRoot
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; }

        public int TargetUserId { get; set; }
        
        [ForeignKey(nameof(TargetUserId))]
        public virtual ApplicationUser TargetUser { get; set; }
    }
}
