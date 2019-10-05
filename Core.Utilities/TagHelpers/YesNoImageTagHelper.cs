using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core.Utilities.TagHelpers
{
    [HtmlTargetElement("yesnoimage", TagStructure = TagStructure.WithoutEndTag)]
    public class YesNoImageTagHelper : TagHelper
    {
        [HtmlAttributeName("value")]
        public string Value { get; set; }
       
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            sb.Append(GetHtml());
            output.PreContent.SetHtmlContent(sb.ToString());
        }
        public string GetHtml() 
        {
            return Value?.ToUpper() == "Y" || Value?.ToUpper() =="TRUE" ? "<div class=\"text-success\"><i class=\"fa fa-check fa-lg\"></i></div>": "<div class=\"text-danger\"><i class=\"fa fa-times fa-lg\"></i></div>";
        }
    }
}