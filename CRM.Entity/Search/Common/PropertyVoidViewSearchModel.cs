using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Common;

namespace CRM.Entity.Search.Common
{
    public class PropertyVoidViewSearchModel : BaseFilterModel
    {
        public PropertyVoidViewSearchModel()
        {
            PropertyVoidViewSearchResult = new List<PropertyVoidViewDto> ();
        }
        public List<PropertyVoidViewDto> PropertyVoidViewSearchResult { get; set; }
        public string PropertyCode { get; set; }
    }
}