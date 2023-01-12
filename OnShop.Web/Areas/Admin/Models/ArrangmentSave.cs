using Microsoft.AspNetCore.Mvc.Rendering;
using OnShop.Domain.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnShop.Web.Areas.Admin.Models
{
    public class ArrangmentSave
    {
        [Display(Name = "اولویت نمایش")]
        public DisplayPriority Priority { get; set; }
        [Display(Name = "نوع")]
        public ArrangementItems Type { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        public long Id { get; set; }

    }
}
