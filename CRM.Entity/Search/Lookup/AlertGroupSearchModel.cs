using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class AlertGroupSearchModel : BaseFilterModel
    {
        public AlertGroupSearchModel()
        {
            AlertGroupSearchResult = new List<AlertGroupDto>();
        }
        public List<AlertGroupDto> AlertGroupSearchResult { get; set; }

       
    }
}