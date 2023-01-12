using System.Collections.Generic;
using OnShop.Domain.Common;
using OnShop.Domain.Interfaces;

namespace OnShop.Domain.Product.Entities
{
    public class Product : BaseUserEntity<long>, IAggregateRoot
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string EnglishTitle { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public decimal? PriceDiscount { get; set; }
        public int? Discount { get; set; }
        public bool Displayed { get; set; } = true;
        public bool CanPurchase { get; set; } = true;
        public int Quantity { get; set; }
        public int? ViewCount { get; set; }
        public int BrandId { get; set; }
        public long CategoryId { get; set; }
        public string Tag { get; set; }

        #region Relationships
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductFeature> ProductFeatures { get; set; }
        public virtual ICollection<ProductTechnical> ProductTechnicals { get; set; }
        #endregion
    }
}