using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class PersonCaseTypeSearchModel : BaseFilterModel
    {
        public PersonCaseTypeSearchModel()
        {
            CaseTypeSearchResult = new List<PersonCaseTypeDto>();
        }
        public List<PersonCaseTypeDto> CaseTypeSearchResult { get; set; }

       
    }
}