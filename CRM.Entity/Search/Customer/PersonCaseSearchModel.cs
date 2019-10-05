using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Customer;

namespace CRM.Entity.Search.Customer
{
    public class PersonCaseSearchModel : BaseFilterModel
    {
        public PersonCaseSearchModel()
        {
            PersonCaseSearchResult = new List<PersonCaseDto>();
        }
        public List<PersonCaseDto> PersonCaseSearchResult { get; set; }
        public int PersonId { get; set; }
    }
}