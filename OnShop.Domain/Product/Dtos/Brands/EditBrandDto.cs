using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using OnShop.Resources.Resources;

namespace OnShop.Domain.Product.Dtos.Brands
{
    public class EditBrandDto
    {
        public int Id { get; set; }

        [MaxLength(200, ErrorMessage = SharedResource.MaxLength)]
        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Title)]
        public string Title { get; set; }

        [MaxLength(200, ErrorMessage = SharedResource.MaxLength)]
        [DisplayName(SharedResource.EnglishTitle)]
        public string EnglishTitle { get; set; }


        [MaxLength(500, ErrorMessage = SharedResource.MaxLength)]
        [DisplayName(SharedResource.ShortDescription)]

        public string Description { get; set; }

        public string Src { get; set; }

        [DisplayName(SharedResource.Photo)]
        public IFormFile File { get; set; }
    }
}