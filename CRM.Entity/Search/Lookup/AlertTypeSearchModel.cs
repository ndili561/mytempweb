using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class AlertTypeSearchModel : BaseFilterModel
    {
        public AlertTypeSearchModel()
        {
            AlertTypeSearchResult = new List<AlertTypeDto>();
        }
        public List<AlertTypeDto> AlertTypeSearchResult { get; set; }

       
    }
}