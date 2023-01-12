using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using OnShop.Domain.DTOs;
using OnShop.Resources.Resources;

namespace OnShop.Domain.Slider.Dtos
{
    public class SliderDto : BaseModelDto<long>
    {
        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Title)]
        public string Title { get; set; }

        public string Src { get; set; }

        [MaxLength(500, ErrorMessage = SharedResource.MaxLength)]
        [DisplayName(SharedResource.ShortDescription)]
        public string Description { get; set; }

        public string LinkTitle { get; set; }

        [DisplayName(SharedResource.Src)]
        public string Link { get; set; }


        [DisplayName(SharedResource.Photo)]
        [Required(ErrorMessage = SharedResource.Required)]
        public IFormFile File { get; set; }
    }
    public  class EditSliderDto : SliderDto
    {
        [DisplayName(SharedResource.Photo)]
        public new IFormFile File { get; set; }
    }
}