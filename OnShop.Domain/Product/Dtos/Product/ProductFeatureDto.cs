using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using OnShop.Resources.Resources;

namespace OnShop.Domain.Product.Dtos.Product
{
    public class ProductFeatureDto
    {
        [MaxLength(200, ErrorMessage = SharedResource.MaxLength)]
        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Title)]
        public string Title { get; set; }

        [MaxLength(200, ErrorMessage = SharedResource.MaxLength)]
        [DisplayName(SharedResource.EnglishTitle)]
        public string EnglishTitle { get; set; }

        [MaxLength(1500, ErrorMessage = SharedResource.MaxLength)]
        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Description)]
        public string Description { get; set; }
    }
    public  class ProductTechnicalDto
    {
        [MaxLength(200, ErrorMessage = SharedResource.MaxLength)]
        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Title)]
        public string Title { get; set; }

        [MaxLength(1500, ErrorMessage = SharedResource.MaxLength)]
        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Description)]
        public string Description { get; set; }
    }
}