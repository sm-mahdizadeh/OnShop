using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnShop.Resources.Resources;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnShop.Web.Areas.Admin.Models
{
    public class PostViewModel
    {
        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Title)]
        public string Title { get; set; }

        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Description)]
        public string Description { get; set; }
        
        [DisplayName("محتوا")]
        [Required(ErrorMessage = SharedResource.Required)]
        public string Content { get; set; }
        public string Tages { get; set; }

        [DisplayName("نمایش داده شود")]
        public bool IsActive { get; set; }
        
        [DisplayName(SharedResource.Photo)]
        [Required(ErrorMessage = SharedResource.Required)]
        public IFormFile File { get; set; }
        public string ImageSrc { get; set; }

        [DisplayName(SharedResource.Category)]
        [Required(ErrorMessage = SharedResource.Required)]
        public int? CategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
