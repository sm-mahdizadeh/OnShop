using OnShop.Domain.Common;
using OnShop.Domain.Enum;
using OnShop.Domain.Interfaces;
using OnShop.Domain.Stores.Entities;

namespace OnShop.Domain.Arrangements.Entities
{
    public class Arrangement : BaseUserEntity<long>, IAggregateRoot
    {
        public long StoreId { get; set; }
        public ArrangementItems Type { get; set; }
        public DisplayPriority Priority { get; set; }
        public long? TargetId { get; set; }
        public string Description { get; set; }

        #region RelationShip

        public Store Store { get; set; }

        #endregion
    }
}
