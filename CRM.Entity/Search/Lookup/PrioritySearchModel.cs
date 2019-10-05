using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class PrioritySearchModel : BaseFilterModel
    {
        public PrioritySearchModel()
        {
            PrioritySearchResult = new List<PriorityDto>();
        }
        public List<PriorityDto> PrioritySearchResult { get; set; }

       
    }
}