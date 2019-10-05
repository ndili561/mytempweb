using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Common;

namespace CRM.Entity.Search.Common
{
    public class PropertyVoidSearchModel : BaseFilterModel
    {
        public PropertyVoidSearchModel()
        {
            PropertyVoidSearchResult = new List<PropertyVoidDto>();
        }
        public List<PropertyVoidDto> PropertyVoidSearchResult { get; set; }
        public string PropertyCode { get; set; }
    }
}