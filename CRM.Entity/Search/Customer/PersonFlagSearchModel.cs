using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Customer;

namespace CRM.Entity.Search.Customer
{
    public class PersonFlagSearchModel : BaseFilterModel
    {
        public PersonFlagSearchModel()
        {
            PersonFlagSearchResult = new List<PersonFlagDto>();
        }
        public List<PersonFlagDto> PersonFlagSearchResult { get; set; }
        public int PersonId { get; set; }
    }
}