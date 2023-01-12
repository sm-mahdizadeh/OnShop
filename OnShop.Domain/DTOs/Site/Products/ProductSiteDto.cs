using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using OnShop.Resources.Resources;

namespace OnShop.Domain.DTOs.Site.Products
{
    public class ProductSiteDto : BaseModelDto<long>
    {
        [DisplayName(SharedResource.Code)]
        public string Code { get; set; }

        [DisplayName(SharedResource.Title)]
        public string Title { get; set; }

        [DisplayName(SharedResource.EnglishTitle)]
        public string EnglishTitle { get; set; }

        [DisplayName(SharedResource.ShortDescription)]
        public string ShortDescription { get; set; }

        public string Description { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal OldPrice { get; set; }
        public int Discount { get; set; } = 0;
        [DisplayName(SharedResource.Price)]
        public decimal Price { get; set; }
        public int Star { get; set; }
        public List<string> ImageSrc { get; set; }
        public int Quantity { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public int BrandId { get; set; }
        public string Available { get; set; }
        public string Tag { get; set; }

        public bool CanPurchase { get; set; }
    }
}