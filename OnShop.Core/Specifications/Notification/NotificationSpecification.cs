using Ardalis.Specification;

namespace OnShop.ApplicationServices.Specifications.Notification
{
    public sealed class NotificationSpecification : Specification<Domain.Notifications.Entities.Notification>
    {
        public NotificationSpecification()
        {

        }
        public NotificationSpecification(bool isRead)
        {
            Query.Where(x => x.IsRead == isRead);
        }
        public NotificationSpecification(long id)
        {
            Query.Where(x => x.Id == id);
        }
    }
}