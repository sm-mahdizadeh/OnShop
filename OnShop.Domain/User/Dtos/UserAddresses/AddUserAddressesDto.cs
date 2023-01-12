using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnShop.Resources.Resources;

namespace OnShop.Domain.User.Dtos.UserAddresses
{
    public class AddUserAddressesDto
    {
        public int ApplicationUserId { get; set; }
        [MaxLength(10, ErrorMessage = SharedResource.MaxLength)]
        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.PostCode)]
        public string PostCode { get; set; }
        
        [MaxLength(10, ErrorMessage = SharedResource.MaxLength)]
        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Plaque)]
        public string Plaque { get; set; }

        [MaxLength(10, ErrorMessage = SharedResource.MaxLength)]
        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Unit)]
        public string Unit { get; set; }

        [MaxLength(256, ErrorMessage = SharedResource.MaxLength)]
        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.PostalAddress)]
        public string PostalAddress { get; set; }
        
        public bool? RecipientIsSelf { get; set; }

        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Districts)]
        public long DistrictId { get; set; }
        
        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Zones)]
        public long ZoneId { get; set; }
        
        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Provience)]
        public long ProvinceId { get; set; }
        public List<SelectListItem> Zones { get; set; }
        public List<SelectListItem> Provinces { get; set; }
        public List<SelectListItem> Districts { get; set; }

    }
}