using OnShop.Domain.Common;
using OnShop.Domain.Interfaces;

namespace OnShop.Domain.Basket.Entities
{
    public class CartItem : BaseUserEntity<long>, IAggregateRoot
    {
        public long ProductId { get; set; }
        public long CartId { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }
        public decimal? PriceDiscount { get; set; }
        public decimal FinalPrice { get; set; }

        #region Relationship
        public virtual Product.Entities.Product Product { get; set; }
        public virtual Cart Cart { get; set; }
        #endregion
    }
}