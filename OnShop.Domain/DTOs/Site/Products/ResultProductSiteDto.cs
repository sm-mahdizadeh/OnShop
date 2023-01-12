using System.Collections.Generic;
using System.ComponentModel;
using OnShop.Resources.Resources;

namespace OnShop.Domain.DTOs.Site.Products
{
    public class ResultProductSiteDto
    {
        public IReadOnlyList<ProductSiteDto> Products { get; set; }
        public int TotalRow { get; set; }
    }
    public class ResultProductDetailsSiteDto
    {
        public ProductSiteDto Product { get; set; }
        public ProductAddToCart ProductAddToCart { get; set; }
        public List<ProductFeaturesSiteDto> ProductFeatures { get; set; }
        public List<ResultProductTechnicalSiteDto> ProductTechnical { get; set; }
        
    }
    public class ResultProductTechnicalSiteDto : BaseModelDto<long>
    {
        [DisplayName(SharedResource.Title)]
        public string Title { get; set; }

        [DisplayName(SharedResource.EnglishTitle)]
        public string EnglishTitle { get; set; }

        [DisplayName(SharedResource.Description)]
        public string Description { get; set; }

        public long ProductId { get; set; }
    }
    public class ProductFeaturesSiteDto : BaseModelDto<long>
    {
        [DisplayName(SharedResource.Title)]
        public string Title { get; set; }

        [DisplayName(SharedResource.EnglishTitle)]
        public string EnglishTitle { get; set; }

        [DisplayName(SharedResource.Description)]
        public string Description { get; set; }
        public long ProductId { get; set; }

    }
    public class ProductAddToCart
    {
        public long ProductId { get; set; }
        public int Count { get; set; }
    }
}
