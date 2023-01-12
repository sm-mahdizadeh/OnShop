using OnShop.Domain.Interfaces;
using OnShop.Domain.User.Commands;
using OnShop.Domain.User.Entities;

namespace OnShop.Domain.User.Repositories
{
    public interface IApplicationUserInfoCommandRepository : IAsyncRepository<UserInfo>
    {
        void Add(UserInfo command);
        void UpdateRep(UserInfo entity, UpdateApplicationUserInfoCommand model);
    }
}