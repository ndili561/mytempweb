using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Customer;

namespace CRM.Entity.Search.Customer
{
    public class PersonEmailSearchModel : BaseFilterModel
    {
        public PersonEmailSearchModel()
        {
            PersonEmailSearchResult = new List<PersonEmailDto>();
        }
        public List<PersonEmailDto> PersonEmailSearchResult { get; set; }
        public int PersonId { get; set; }
    }
  
}