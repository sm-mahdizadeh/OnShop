using MediatR;
using OnShop.Framework.Commands;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.User.Commands
{
    public class UserAddressCommand
    {

    }
    public class DeleteUserAddressCommand : IRequest<ResultDto>
    {
        public long Id { get; set; }
    }

    public class AddUserAddressCommand : BaseCommandEntity, IRequest<ResultDto>
    {
        public int ApplicationUserId { get; set; }
        public string PostCode { get; set; }
        public string Plaque { get; set; }
        public string Unit { get; set; }
        public string PostalAddress { get; set; }
        public bool? RecipientIsSelf { get; set; }
        public long DistrictId { get; set; }
    }
}