using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace OnShop.Web.Common.TagHelpers
{
    /// <summary>
    /// <see cref="ITagHelper"/> implementation targeting &lt;enum-radio-button&gt; elements with an <c>asp-for</c> attribute, <c>value</c> attribute.
    /// </summary>
    [HtmlTargetElement("enum-radio-button", Attributes = RadioButtonEnumForAttributeName)]
    public class RadioButtonEnumTagHelper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
    {
        private const string RadioButtonEnumForAttributeName = "asp-for";
        private const string RadioButtonEnumValueAttributeName = "value";

        /// <summary>
        /// Creates a new <see cref="RadioButtonEnumTagHelper"/>.
        /// </summary>
        /// <param name="generator">The <see cref="IHtmlGenerator"/>.</param>
        public RadioButtonEnumTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        /// <inheritdoc />
        public override int Order => -1000;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator Generator { get; }

        /// <summary>
        /// An expression to be evaluated against the current model.
        /// </summary>
        [HtmlAttributeName(RadioButtonEnumForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName(RadioButtonEnumValueAttributeName)]
        public Enum value { get; set; }

        /// <inheritdoc />
        /// <remarks>Does nothing if <see cref="For"/> is <c>null</c>.</remarks>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {

            var childContent = await output.GetChildContentAsync();
            string innerContent = childContent.GetContent();
            output.Content.AppendHtml(innerContent);

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "list-group");

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            var modelExplorer = For.ModelExplorer;
            var metaData = For.Metadata;

            foreach (var item in metaData.EnumNamesAndValues)
            {
                if (metaData.ContainerType is not null)
                {
                    var enumId = $"{metaData.ContainerType.Name}_{metaData.PropertyName}_{item.Key}";

                    var enumIschecked = false;
                    if (value != null)
                    {
                        if (value != null && item.Key == value.ToString())
                        {
                            enumIschecked = true;
                        }
                    }
                    else
                    {
                        if (For.Model != null && item.Key == For.Model.ToString())
                        {
                            enumIschecked = true;
                        }
                    }

                    var enumInputLabelName = item.Key;
                    var enumResourcedName = metaData.EnumGroupedDisplayNamesAndValues.FirstOrDefault(x => x.Value == item.Value);
                    if (enumResourcedName.Value != null)
                    {
                        enumInputLabelName = enumResourcedName.Key.Name;
                    }

                    var enumLabel = Generator.GenerateLabel(
                        ViewContext,
                        For.ModelExplorer,
                        For.Name,
                        enumInputLabelName,
                        htmlAttributes: new { @for = enumId, Class = "list-group-item" });

                    var enumRadio = Generator.GenerateRadioButton(
                        ViewContext,
                        For.ModelExplorer,
                        metaData.PropertyName,
                        item.Key,
                        false,
                        htmlAttributes: new { id = enumId });

                    enumRadio.Attributes.Remove("checked");
                    enumRadio.AddCssClass("form-check-input me-1");
                    if (enumIschecked)
                    {
                        enumRadio.MergeAttribute("checked", "checked");
                    }
                    enumLabel.InnerHtml.AppendHtml(enumRadio);
                    output.Content.AppendHtml(enumLabel);
                }
            }

            var validationMsg = Generator.GenerateValidationMessage(
                ViewContext,
                For.ModelExplorer,
                For.Name,
                null,
                ViewContext.ValidationMessageElement,
                new { @class = "text-danger" });
            output.Content.AppendHtml(validationMsg);
        }
    }
}