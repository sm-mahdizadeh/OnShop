using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnShop.Web.Common.TagHelpers
{
    [HtmlTargetElement("ProductStar")]
    public class ProductStarRateHealper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
    {
        private IUrlHelper _urlHelper;
        public ProductStarRateHealper(IActionContextAccessor actionContextAccessor, IUrlHelperFactory urlHelperFactory)
        {
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        [HtmlAttributeName("Rate")]
        public int Rate { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("class", $"product_ratings");
            string s = "<ul>";
            for (var i = 0; i < Rate; i++)
            {
                s += "< li >< a  >< i class='ion-sta'></i></a></li>";
            }
            s += "</ul>";
            output.Content.SetContent(s);


        }
    }

}
