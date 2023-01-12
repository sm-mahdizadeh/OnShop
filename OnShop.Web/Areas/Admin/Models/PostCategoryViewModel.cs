using Microsoft.AspNetCore.Http;
using OnShop.Resources.Resources;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnShop.Web.Areas.Admin.Models
{
    public class PostCategoryViewModel
    {
        [DisplayName(SharedResource.Title)]
        [Required(ErrorMessage = SharedResource.Required)]
        public string Title { get; set; }

        public int Id { get; set; }
    }
}
