using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using OnShop.Resources.Resources;

namespace OnShop.Domain.User.Dtos
{
    public class ForgetPasswordDto
    {
        [DisplayName(SharedResource.Email)]
        [EmailAddress(ErrorMessage = SharedResource.EmailError)]
        [Required(ErrorMessage = SharedResource.Required)]
        public string Email { get; set; }
    }
}