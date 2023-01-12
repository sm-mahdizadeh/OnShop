using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace OnShop.Web.Common.TagHelpers
{
    [HtmlTargetElement("input", Attributes = ForAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class InvariantDecimalTagHelper : InputTagHelper
    {
        private const string ForAttributeName = "asp-for";

        private IHtmlGenerator _generator;

        [HtmlAttributeName("asp-is-invariant")]
        public bool IsInvariant { set; get; }

        public InvariantDecimalTagHelper(IHtmlGenerator generator) : base(generator)
        {
            _generator = generator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (IsInvariant && output.TagName == "input" && For.Model != null && For.Model is decimal)
            {
                var value = (decimal)(For.Model);
                var invariantValue = $"{value:n0}";
                output.Attributes.SetAttribute(new TagHelperAttribute("value", invariantValue));
            }
        }
    }
}