using System.Collections.Generic;
using OnShop.Domain.Basket.Entities;
using OnShop.Domain.Common;
using OnShop.Domain.Enum;
using OnShop.Domain.Interfaces;
using OnShop.Domain.Payments.Entities;
using OnShop.Domain.User.Entities;

namespace OnShop.Domain.Orders.Entities
{
    public class Order : BaseUserEntity<long>, IAggregateRoot
    {
        public long UserAddressId { get; set; }
        public OrderPostType OrderPostType { get; set; }
        public OrderState OrderState { get; set; }
        public long CartId { get; set; }

        #region  Relationship
        public virtual UserAddress UserAddress { get; set; }
        public virtual List<Payment> Payments { get; set; }
        public virtual Cart Cart { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        #endregion


    }
}