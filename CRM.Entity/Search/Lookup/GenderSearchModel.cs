using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class GenderSearchModel : BaseFilterModel
    {
        public GenderSearchModel()
        {
            GenderSearchResult = new List<GenderDto>();
        }
        public List<GenderDto> GenderSearchResult { get; set; }

       
    }
}