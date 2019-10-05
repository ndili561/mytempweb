using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class EthnicitySearchModel : BaseFilterModel
    {
        public EthnicitySearchModel()
        {
            EthnicitySearchResult = new List<EthnicityDto>();
        }
        public List<EthnicityDto> EthnicitySearchResult { get; set; }

       
    }
}