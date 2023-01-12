using MediatR;
using OnShop.Framework.Commands;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Stores.Commands
{
    public class StoreCreateCommand : BaseCommandEntity,IRequest<ResultDto>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int StoreType { get; set; } = 1;
        public int MembershipType { get; set; } = 1;
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string BigLogo { get; set; }
        public string SmallLogo { get; set; }
    }
}