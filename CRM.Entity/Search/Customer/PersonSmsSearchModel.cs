using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Customer;

namespace CRM.Entity.Search.Customer
{
    public class PersonSmsSearchModel : BaseFilterModel
    {
        public PersonSmsSearchModel()
        {
            PersonSmsSearchResult = new List<PersonSmsDto>();
        }
        public List<PersonSmsDto> PersonSmsSearchResult { get; set; }
        public int PersonId { get; set; }
    }
  
}