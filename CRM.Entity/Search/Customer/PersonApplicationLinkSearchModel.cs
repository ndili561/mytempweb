using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Customer;

namespace CRM.Entity.Search.Customer
{
    public class PersonApplicationLinkSearchModel : BaseFilterModel
    {
        public PersonApplicationLinkSearchModel()
        {
            PersonApplicationLinkSearchResult = new List<PersonApplicationLinkDto>();
        }
        public List<PersonApplicationLinkDto> PersonApplicationLinkSearchResult { get; set; }

       
    }
}