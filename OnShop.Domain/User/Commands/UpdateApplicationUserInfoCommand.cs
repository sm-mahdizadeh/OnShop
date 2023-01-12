using System;
using MediatR;
using OnShop.Framework.Commands;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.User.Commands
{
    public class UpdateApplicationUserInfoCommand : BaseCommandEntity, IRequest<ResultDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public bool? Gender { get; set; }
    }
}