using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class LanguageSearchModel : BaseFilterModel
    {
        public LanguageSearchModel()
        {
            LanguageSearchResult = new List<LanguageDto>();
        }
        public List<LanguageDto> LanguageSearchResult { get; set; }

       
    }
}