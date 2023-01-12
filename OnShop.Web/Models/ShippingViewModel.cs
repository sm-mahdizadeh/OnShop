using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using OnShop.Domain.Basket.Dtos;
using OnShop.Domain.Enum;
using OnShop.Domain.User.Dtos.UserAddresses;
using OnShop.Resources.Resources;

namespace OnShop.Web.Models
{
    public class ShippingViewModel
    {
        public CartPayDto CartPayDto { get; set; }
        public List<UserAddressDto> UserAddressDtos { get; set; }

        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.Address)] 
        public int UserAddressId { get; set; }
        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.OrderPostType)]
        public OrderPostType OrderPostType { get; set; }
        
        [Required(ErrorMessage = SharedResource.Required)]
        [DisplayName(SharedResource.PaymentType)]
        public PaymentType PaymentType { get; set; }
    }
}