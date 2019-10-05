using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class FlagGroupSearchModel : BaseFilterModel
    {
        public FlagGroupSearchModel()
        {
            FlagGroupSearchResult = new List<FlagGroupDto>();
        }
        public List<FlagGroupDto> FlagGroupSearchResult { get; set; }

       
    }
}