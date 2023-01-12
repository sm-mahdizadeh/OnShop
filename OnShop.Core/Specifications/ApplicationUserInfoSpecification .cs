using Ardalis.Specification;
using OnShop.Domain.User.Entities;

namespace OnShop.ApplicationServices.Specifications
{
    public sealed class ApplicationUserInfoSpecification : Specification<UserInfo>
    {
        public ApplicationUserInfoSpecification(int applicationUserId)
        {
            Query.Where(x => x.ApplicationUserId == applicationUserId);
        }
    }
}