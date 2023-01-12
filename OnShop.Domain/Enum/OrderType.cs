using System.ComponentModel.DataAnnotations;

namespace OnShop.Domain.Enum
{
    public enum OrderType
    {
        [Display(Name = "ascending")]
        Ascending = 1,
        [Display(Name = "descending")]
        Descending
    }
}