using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class PersonCaseStatusSearchModel : BaseFilterModel
    {
        public PersonCaseStatusSearchModel()
        {
            CaseStatusSearchResult = new List<PersonCaseStatusDto>();
        }
        public List<PersonCaseStatusDto> CaseStatusSearchResult { get; set; }

       
    }
}