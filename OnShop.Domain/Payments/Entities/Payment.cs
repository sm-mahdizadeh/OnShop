using System;
using OnShop.Domain.Common;
using OnShop.Domain.Enum;
using OnShop.Domain.Interfaces;
using OnShop.Domain.Orders.Entities;

namespace OnShop.Domain.Payments.Entities
{
    public class Payment : BaseUserEntity<Guid>, IAggregateRoot
    {
        public decimal Amount { get; set; }
        public bool IsPay { get; set; }
        public DateTime? PayDate { get; set; }
        public string Authority { get; set; }
        public long RefId { get; set; } = 0;
        public PaymentType PaymentType { get; set; }
        public long OrderId { get; set; }
        public string StatusCode { get; set; }
        public string StatusCodeMessage { get; set; }
        #region Relationship
        public virtual Order Order { get; set; }
        #endregion

    }
}