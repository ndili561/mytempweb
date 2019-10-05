using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core.Utilities.TagHelpers
{
    [HtmlTargetElement("search", TagStructure = TagStructure.WithoutEndTag)]
    public class SearchTagHelper : TagHelper
    {
        [HtmlAttributeName("model")]
        public BaseFilterModel Model { get; set; }
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            Model = Model ?? (BaseFilterModel)ViewContext.ViewData.Model;
            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            sb.Append(Search());
            output.PreContent.SetHtmlContent(sb.ToString());
        }
        public string Search() 
        {
            var htmlString= new StringBuilder();
            htmlString.AppendLine("<div class='col-md-12'>");
            htmlString.AppendLine("<div class='input-group'>");

            htmlString.AppendLine("<input type='text' id='FilterText' name='FilterText' class='input-sm form-control hasclear filterInput' list='terms' data_default_button='.btn-filter' autofocus='' data_default_focus='true' data_target='#container' placeholder='Enter search text here and press enter...' />");
            htmlString.AppendLine("<span class='input-group-btn'>");
            htmlString.AppendLine($"<a targetgridid = '{Model.TargetGridId}' id = 'btn_{Model.TargetGridId }' class='btn btn-sm btn-primary btn-filter' href='#'> <i class='fa fa-binoculars fa-lg'></i></a>");
            htmlString.AppendLine("<button class='btn btn-sm btn-warning' type='button' onclick='clearStorage(this)'><i class='fa fa-refresh fa-lg'></i></button>");
            htmlString.AppendLine("</span>");

            htmlString.AppendLine("</div>");
            htmlString.AppendLine("</div>");

            return htmlString.ToString();
        }

       
    }

   
}