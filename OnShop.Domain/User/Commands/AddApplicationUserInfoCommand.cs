using System;
using MediatR;
using OnShop.Framework.Commands;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.User.Commands
{
    public class AddApplicationUserInfoCommand : BaseCommandEntity, IRequest<ResultDto>
    {
        public int ApplicationUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public DateTime? Birthdate { get; set; }
        public bool? Gender { get; set; }
    }
}