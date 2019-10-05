using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Customer;

namespace CRM.Entity.Search.Customer
{
    public class PersonAddressSearchModel : BaseFilterModel
    {
        public PersonAddressSearchModel()
        {
            PersonAddressSearchResult = new List<PersonAddressDto>();
        }
        public List<PersonAddressDto> PersonAddressSearchResult { get; set; }
    }
}