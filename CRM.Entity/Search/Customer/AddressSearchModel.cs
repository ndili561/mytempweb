using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Customer;

namespace CRM.Entity.Search.Customer
{
    public class AddressSearchModel : BaseFilterModel
    {
        public AddressSearchModel()
        {
            AddressSearchResult = new List<AddressDto>();
        }
        public List<AddressDto> AddressSearchResult { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string PostCode { get; set; }
    }
}