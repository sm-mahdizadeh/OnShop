using System;
using System.Collections.Generic;
using OnShop.Domain.Common;
using OnShop.Domain.Interfaces;
using OnShop.Domain.Orders.Entities;

namespace OnShop.Domain.Basket.Entities
{
    public class Cart : BaseUserEntity<long>, IAggregateRoot
    {

        public Guid BrowserId { get; set; }
        public bool IsFinished { get; set; }
        public string UserIp { get; set; }
        public string BrowserName { get; set; }

        
        #region Relationship
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual Order Order { get; set; }

        #endregion

    }
}