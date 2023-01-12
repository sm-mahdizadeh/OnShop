using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using OnShop.Resources.Resources;

namespace OnShop.Domain.User.Dtos
{
    public class RegisterUserDto
    {
        [DisplayName(SharedResource.Email)]
        [EmailAddress(ErrorMessage = SharedResource.EmailError)]
        [Required(ErrorMessage = SharedResource.Required)]
        public string Email { get; set; }

        [DisplayName(SharedResource.Phone)]
        [Phone(ErrorMessage = SharedResource.PhoneError)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = SharedResource.PhoneError)]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = SharedResource.Required)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        [DisplayName(SharedResource.Password)]
        [Required(ErrorMessage = SharedResource.Required)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName(SharedResource.RePassword)]
        [Compare(nameof(Password), ErrorMessage = SharedResource.RePasswordError)]
        [Required(ErrorMessage = SharedResource.Required)]
        public string RePassword { get; set; }

        [DisplayName(SharedResource.FirstName)]
        [Required(ErrorMessage = SharedResource.Required)]
        public string FirstName { get; set; }
        
        [DisplayName(SharedResource.LastName)]
        [Required(ErrorMessage = SharedResource.Required)]
        public string LastName { get; set; }

        public DateTime RegisteredDate { get; set; }
    }
}