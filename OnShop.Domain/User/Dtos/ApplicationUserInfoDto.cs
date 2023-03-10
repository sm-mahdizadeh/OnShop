using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using OnShop.Resources.Resources;

namespace OnShop.Domain.User.Dtos
{
    public class ApplicationUserInfoDto
    {
        [DisplayName()]
        public int ApplicationUserId { get; set; }

        [DisplayName(SharedResource.FirstName)]
        [Required(ErrorMessage = SharedResource.Required)]
        public string FirstName { get; set; }

        [DisplayName(SharedResource.LastName)]
        [Required(ErrorMessage = SharedResource.Required)]
        public string LastName { get; set; }

        [DisplayName(SharedResource.NationalCode)]
        [Required(ErrorMessage = SharedResource.Required)]
        public string NationalCode { get; set; }

        [DisplayName(SharedResource.Birthdate)]
        public DateTime? Birthdate { get; set; }

        [DisplayName(SharedResource.Sex)]
        public string GenderString { get; set; }

        public bool IsUpdated { get; set; }

        public bool? Gender { get; set; }

    }
}