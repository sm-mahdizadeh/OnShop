using System.Collections.Generic;
using System.ComponentModel;
using OnShop.Domain.Product.Dtos.Brands;
using OnShop.Domain.Product.Dtos.Categories;
using OnShop.Resources.Resources;

namespace OnShop.Domain.DTOs.Site.Products
{
    public class ProductFilters
    {
        public IReadOnlyList<GetBrandDto> Brands { get; set; }
        public IReadOnlyList<GetCategoryDto> Categories { get; set; }

        [DisplayName(SharedResource.Available)]
        public bool Available { get; set; }

        public decimal MaxAmount { get; set; }
    }
}