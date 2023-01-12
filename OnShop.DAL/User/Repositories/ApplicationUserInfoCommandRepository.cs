using System;
using OnShop.DAL.Context;
using OnShop.DAL.Data;
using OnShop.Domain.User.Commands;
using OnShop.Domain.User.Entities;
using OnShop.Domain.User.Repositories;

namespace OnShop.DAL.User.Repositories
{
    public class ApplicationUserInfoCommandRepository : EfRepository<UserInfo>, IApplicationUserInfoCommandRepository
    {

        public ApplicationUserInfoCommandRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public void Add(UserInfo command)
        {
            DbContext.UserInfos.Add(command);
        }

        public void UpdateRep(UserInfo entity, UpdateApplicationUserInfoCommand model)
        {
            entity.Gender = model.Gender;
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Birthdate = model.Birthdate;
            entity.ModifiedId = model.ModifiedId;
            entity.ModifiedDate = DateTime.Now;

        }
    }
}