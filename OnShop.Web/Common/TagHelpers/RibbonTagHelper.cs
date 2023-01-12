using Microsoft.AspNetCore.Razor.TagHelpers;

namespace OnShop.Web.Common.TagHelpers
{
    [HtmlTargetElement("ribbon")]

    public class RibbonTagHelper : TagHelper
    {
 
        public string Title { get; set; }
        public RibbonStyle Style { get; set; } = RibbonStyle.one;
        public RibbonType Type { get; set; } = RibbonType.primary;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            var childContext = output.GetChildContentAsync().Result;
            var content = childContext.GetContent();
            string x = "";
            if (Style == RibbonStyle.two)
                x = "-two";
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "row");
            output.Content.SetHtmlContent($@"

    <div class='col-12'>
        <div class='box'>
            <div class='box-body ribbon-box'>
                <div class='ribbon{x} ribbon{x}-{Type}'><span>{Title}</span></div>
{content}
            </div>
        </div>
    </div>
");


        }

    }


    public enum RibbonType
    {
        dark,
        primary,
        success,
        info,
        warning,
        danger,
    }
    public enum RibbonStyle
    {
        one,
        two,
    }
}