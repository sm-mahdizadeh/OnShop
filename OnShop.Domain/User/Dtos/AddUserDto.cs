using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnShop.Resources.Resources;

namespace OnShop.Domain.User.Dtos
{
    public class AddUserDto
    {
        [DisplayName(SharedResource.UserName)]
        [Required(ErrorMessage = SharedResource.Required)]
        public string UserName { get; set; }

        [DisplayName(SharedResource.Email)]
        [EmailAddress(ErrorMessage = SharedResource.EmailError)]
        //[Required(ErrorMessage = SharedResource.Required)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [DisplayName(SharedResource.Password)]
        [Required(ErrorMessage = SharedResource.Required)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName(SharedResource.RePassword)]
        [Compare(nameof(Password), ErrorMessage = SharedResource.RePasswordError)]
        [Required(ErrorMessage = SharedResource.Required)]
        public string RePassword { get; set; }

        public DateTime RegisteredDate { get; set; }

        [DisplayName(SharedResource.Role)]
        [Required(ErrorMessage = SharedResource.Required)]
        public int[] RoleIds { get; set; }

        public List<SelectListItem> ApplicationRoles { get; set; }
    }
}
