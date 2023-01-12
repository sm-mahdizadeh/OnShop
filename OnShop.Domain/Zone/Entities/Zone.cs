using System.Collections.Generic;
using OnShop.Framework.Domain;

namespace OnShop.Domain.Zone.Entities
{
    public class Zone : BaseHcEntity<long>
    {

        #region Relations
        public ICollection<Province> Province { get; set; }
        #endregion
    }
}