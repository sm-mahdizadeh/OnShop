using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Net;

namespace OnShop.Web.Common.TagHelpers
{
    [HtmlTargetElement("marker")]

    public class MarkerTagHelper : TagHelper
    {

        public string Key { get; set; }
        public string Value { get; set; } = null;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            var childContext = output.GetChildContentAsync().Result;
            var content = Value ?? childContext.GetContent();

            if (!string.IsNullOrEmpty(Key))
            {
                content = content.Replace(Key, $"<b style='background-color:yellow'>{Key}</b>");
            }

            //output.TagName = "span";
            //output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent(content);


        }

    }

}