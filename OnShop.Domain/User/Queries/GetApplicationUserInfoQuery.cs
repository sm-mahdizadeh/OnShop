using MediatR;
using OnShop.Domain.User.Dtos;

namespace OnShop.Domain.User.Queries
{
    public class GetApplicationUserInfoQuery : IRequest<ApplicationUserInfoDto>
    {
        public int ApplicationUserId { get; set; }
    }
}