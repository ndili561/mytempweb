using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class FlagTypeSearchModel : BaseFilterModel
    {
        public FlagTypeSearchModel()
        {
            FlagTypeSearchResult = new List<FlagTypeDto>();
        }
        public List<FlagTypeDto> FlagTypeSearchResult { get; set; }

       
    }
}