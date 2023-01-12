using OnShop.DAL.Context;
using OnShop.DAL.Data;
using OnShop.Domain.Notifications.Entities;
using OnShop.Domain.Notifications.Repositories;
using System.Threading.Tasks;

namespace OnShop.DAL.Arrangements.Repositories
{
    public class NotificationRepository : EfRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public Task MarkAsRead(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}