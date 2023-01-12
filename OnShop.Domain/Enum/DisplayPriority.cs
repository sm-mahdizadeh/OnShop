using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnShop.Domain.Enum
{
    public enum DisplayPriority
    {
        [Display(Name="عدم نمایش ")]
        Hide = 0,
        [Display(Name = "اولویت اول ")]
        Priority1 = 1,
        [Display(Name = "اولویت دوم ")]
        Priority2 = 2,
        [Display(Name = "اولویت سوم ")]
        Priority3 = 3,
        [Display(Name = "اولویت چهارم ")]
        Priority4 = 4,
        [Display(Name = "اولویت پنجم ")]
        Priority5 = 5,
        [Display(Name = "اولویت ششم ")]
        Priority6 = 6,
        [Display(Name = "اولویت هفتم ")]
        Priority7 = 7,
        [Display(Name = "اولویت هشتم ")]
        Priority8 = 8,
        [Display(Name = "اولویت نهم ")]
        Priority9 = 9,
        [Display(Name = "اولویت دهم ")]
        Priority10 = 10
    }
}