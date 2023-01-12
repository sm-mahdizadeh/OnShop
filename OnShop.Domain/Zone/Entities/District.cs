using OnShop.Domain.User.Entities;
using OnShop.Framework.Domain;

namespace OnShop.Domain.Zone.Entities
{
    public class District : BaseHcEntity<long>
    {
        public long ProvinceId { get; set; }

        #region relations
        public virtual Province Province { get; set; }
        public virtual UserAddress ApplicationUserAddress { get; set; }
        #endregion
    }
}