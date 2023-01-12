using System.ComponentModel.DataAnnotations;

namespace OnShop.Domain.Enum
{
    public enum Ordering
    {
        [Display(Name = "مرتب سازی بر اساس :")]
        NotOrdered = 0,
        [Display(Name = "بیشترین بازدید")]
        MostVisited = 1,
        [Display(Name = "پرفروش ترین")]
        BestSelling,
        [Display(Name = "محبوب ترین")]
        MostPopular,
        [Display(Name = "جدید ترین")]
        TheNewest,
        [Display(Name = "ارزان ترین")]
        Cheapest,
        [Display(Name = "گران ترین")]
        Expensive,
        [Display(Name = "بیشترین امتیاز")]
        MostStar,
        [Display(Name = "بیشترین تخفیف")]
        MostOffer
    }
}