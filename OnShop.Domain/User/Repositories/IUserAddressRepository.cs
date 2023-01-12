using System.Collections.Generic;
using System.Threading.Tasks;
using OnShop.Domain.Interfaces;
using OnShop.Domain.User.Entities;
using OnShop.Domain.Zone.Entities;

namespace OnShop.Domain.User.Repositories
{
    public interface IUserAddressRepository : IAsyncRepository<UserAddress>
    {
        Task<IReadOnlyList<Zone.Entities.Zone>> ListZoneAllAsync(bool asNoTracking = false);
        Task<IReadOnlyList<Province>> ListProvinceAllAsync(long? zoneId=null,bool asNoTracking = false);
        Task<IReadOnlyList<District>> ListDistrictAllAsync(long? provinceId = null, bool asNoTracking = false);
    }
}