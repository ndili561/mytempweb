using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class NationalityTypeSearchModel : BaseFilterModel
    {
        public NationalityTypeSearchModel()
        {
            NationalityTypeSearchResult = new List<NationalityTypeDto>();
        }
        public List<NationalityTypeDto> NationalityTypeSearchResult { get; set; }

       
    }
}