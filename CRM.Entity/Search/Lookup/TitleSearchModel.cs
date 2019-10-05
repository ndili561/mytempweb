using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class TitleSearchModel : BaseFilterModel
    {
        public TitleSearchModel()
        {
            TitleSearchResult = new List<TitleDto>();
        }
        public List<TitleDto> TitleSearchResult { get; set; }

       
    }
}