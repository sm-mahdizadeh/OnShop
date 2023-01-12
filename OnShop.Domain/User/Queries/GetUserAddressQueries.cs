using System.Collections.Generic;
using MediatR;
using OnShop.Domain.User.Dtos.UserAddresses;

namespace OnShop.Domain.User.Queries
{
    public class GetUserAddressQueries : IRequest<List<UserAddressDto>>
    {
        public int ApplicationUserId { get; set; }
    }
    public class GetZoneQueries : IRequest<IReadOnlyList<DropDownDto>>
    {
    }
    public class GetProvinceQueries : IRequest<IReadOnlyList<DropDownDto>>
    {
        public int? ZoneId { get; set; }
    }
    public class GetDistrictQueries : IRequest<IReadOnlyList<DropDownDto>>
    {
        public int? ProvinceId { get; set; }
    }
}