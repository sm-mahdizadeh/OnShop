using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnShop.Domain.Enum
{
    public enum ArrangementItems
    {
        [Display(Name = "اسلایدر")]
        Slider =1000,
        ProductSmallNew=2001,
        ProductSmallFavorite = 2002,
        ProductSmallMoreSell = 2003,
        ProductSmallMoreView = 2004,
        ProductSmallOff = 2005,
        ProductBigNew = 2101,
        ProductBigFavorite = 2102,
        ProductBigMoreSell = 2103,
        ProductBigMoreView = 2104,
        ProductBigOff = 2105,
        Baner1Pic =3001,
        Baner2Pic = 3002,
        Baner3Pic = 3003,
        Baner4Pic = 3004,
        Baner1Item = 3011,
        Baner2Item = 3012,
        Baner3Item = 3013,
        Baner4Item = 3014,
        Category=4000,
        Brand = 4100,
        [Display(Name="خبر")]
        News = 4201,
    }
}
