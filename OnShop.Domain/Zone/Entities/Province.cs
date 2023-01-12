using System.Collections.Generic;
using OnShop.Framework.Domain;

namespace OnShop.Domain.Zone.Entities
{
    public class Province : BaseHcEntity<long>
    {
        public long ZoneId { get; set; }

        #region Relations
        public virtual Zone Zone { get; set; }
        public ICollection<District> District { get; set; }
        #endregion

    }
}