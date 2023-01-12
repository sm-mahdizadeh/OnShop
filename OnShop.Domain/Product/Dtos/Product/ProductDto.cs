using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnShop.Domain.DTOs;
using OnShop.Resources.Resources;

namespace OnShop.Domain.Product.Dtos.Product
{
    public class ProductDto :BaseModelDto<long>
    {
        [MaxLength(10, ErrorMessage = SharedResource.MaxLength)]
        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Code)]
        public string Code { get; set; }

        [MaxLength(200, ErrorMessage = SharedResource.MaxLength)]
        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Title)]
        public string Title { get; set; }

        [MaxLength(200, ErrorMessage = SharedResource.MaxLength)]
        [DisplayName(SharedResource.EnglishTitle)]
        public string EnglishTitle { get; set; }

        [MaxLength(int.MaxValue, ErrorMessage = SharedResource.MaxLength)]
        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Description)]
        public string Description { get; set; }

        [MaxLength(1500, ErrorMessage = SharedResource.MaxLength)]
        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.ShortDescription)]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Price)]
        public decimal Price { get; set; }

        //[Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Color)]
        public string Color { get; set; }

        [DisplayName(SharedResource.PriceDiscount)]
        public decimal? PriceDiscount { get; set; }

        [DisplayName(SharedResource.Discount)]
        public int? Discount { get; set; }

        [DisplayName(SharedResource.Displayed)]
        public bool Displayed { get; set; } = true;

        [DisplayName(SharedResource.CanPurchase)]
        public bool CanPurchase { get; set; } = true;

        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Quantity)]
        public int Quantity { get; set; }

        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Brand)]
        public int BrandId { get; set; }

        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Category)]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Photo)]
        public List<IFormFile> Files { get; set; }

        public List<SelectListItem> Category { get; set; }
        public List<SelectListItem> Brand { get; set; }

        public string ProductFeatureDto { get; set; }
        public string ProductTechnical { get; set; }

        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Tag)]
        public string Tag { get; set; }
    }
}