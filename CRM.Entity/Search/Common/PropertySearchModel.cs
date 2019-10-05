using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Common;

namespace CRM.Entity.Search.Common
{
    public class PropertySearchModel : BaseFilterModel
    {
        public PropertySearchModel()
        {
            PropertySearchResult = new List<PropertyDto>();
        }
        public List<PropertyDto> PropertySearchResult { get; set; }
        public string PropertyCode { get; set; }
    }
}