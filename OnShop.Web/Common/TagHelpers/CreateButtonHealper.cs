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
    [HtmlTargetElement("CreateButton")]
    public class CreateButtonHealper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
    {
        private IUrlHelper _urlHelper;
        public CreateButtonHealper( IActionContextAccessor actionContextAccessor, IUrlHelperFactory urlHelperFactory)
        {
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }
        [HtmlAttributeName("area")]
        public string Area { get; set; }

        [HtmlAttributeName("controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("action")]
        public string Action { get; set; } = "Create";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
   
            output.TagName = "a";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("class", $"btn btn-success");
            output.Content.SetContent(" <i class='fa fa-plus'></i> افزودن ");
            output.Attributes.Add("href", _urlHelper.Action(this.Area, this.Action, this.Controller));

        }
    }

}
