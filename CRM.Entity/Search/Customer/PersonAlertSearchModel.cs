using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Customer;

namespace CRM.Entity.Search.Customer
{
    public class PersonAlertSearchModel : BaseFilterModel
    {
        public PersonAlertSearchModel()
        {
            PersonAlertSearchResult = new List<PersonAlertDto>();
        }
        public List<PersonAlertDto> PersonAlertSearchResult { get; set; }
        public int PersonId { get; set; }
    }
}