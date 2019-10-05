using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Core.Utilities.Enum;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json.Linq;

namespace Core.Utilities.TagHelpers
{
    [HtmlTargetElement("audit", TagStructure = TagStructure.WithoutEndTag)]
    public class AuditTagHelper : TagHelper
    {

        [HtmlAttributeName("model")]
        public AuditModel Model { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            string html = CreateHtmlForAudit();
            output.PreContent.SetHtmlContent(html);
        }
        private string CreateHtmlForAudit()
        {
            if (Model == null || Model.NewValues == null || !IsPropertyExist(Model, "NewValues") || !IsPropertyExist(Model, "OldValues"))
                return string.Empty;

            var newValues = JObject.Parse(Model?.NewValues);
            var sb = new StringBuilder();
            foreach (var property in newValues.Properties())
            {
                var origianlValue = string.Empty;
                if (Model.EventType == EventType.Update || Model.EventType == EventType.Deleted)
                {
                    var originalValues = JObject.Parse(Model?.OldValues);
                    foreach (var originalProperty in originalValues.Properties())
                    {
                        if (property.Name == originalProperty.Name)
                        {
                            origianlValue = originalProperty.Value.ToString();
                            break;
                        }
                    }
                }
                string divClass = "class=\"\"";
                string propertyNameClass = "class=\"text text-navy\"";
                if (property.Value.ToString() != origianlValue && Model.EventType == EventType.Update)
                {
                    sb.AppendLine($"<div {divClass}><span {propertyNameClass}><strong>{property.Name}:</strong> </span><span class=\"text-success\">{property.Value??""} </span><span style =\"text-decoration: line-through;\" class=\"text-danger\">{origianlValue}</span></div>");
                }
                else if (Model.EventType == EventType.Insert && !string.IsNullOrWhiteSpace(property.Value?.ToString()))
                {
                    sb.AppendLine($"<div {divClass}><span {propertyNameClass}><strong>{property.Name}:</strong> </span><span class=\"text-success\">{property.Value} </span><span class=\"text-danger\">{origianlValue}</span></div>");
                }
                else if (!string.IsNullOrWhiteSpace(origianlValue) && Model.EventType != EventType.Update)
                {
                    sb.AppendLine($"<div {divClass}><span style \"text-decoration: line-through;\" {propertyNameClass}><strong>{property.Name}: </span><span class=\"text-success\">{property.Value} </span><span class=\"text-danger\">{origianlValue}</span></div>");
                }

            }
            return sb.ToString();
        }
        public static bool IsPropertyExist(dynamic entity, string name)
        {
            if (entity is ExpandoObject)
                return ((IDictionary<string, object>)entity).ContainsKey(name);

            return entity.GetType().GetProperty(name) != null;
        }
    }

    
}