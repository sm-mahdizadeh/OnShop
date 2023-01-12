using OnShop.Domain.Interfaces;
using OnShop.Domain.Notifications.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnShop.Domain.Notifications.Repositories
{
    public interface INotificationRepository : IAsyncRepository<Notification>
    {
        Task MarkAsRead(long id);
    }
}
