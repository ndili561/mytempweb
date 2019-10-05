using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class AddressTypeSearchModel : BaseFilterModel
    {
        public AddressTypeSearchModel()
        {
            AddressTypeSearchResult = new List<AddressTypeDto>();
        }
        public List<AddressTypeDto> AddressTypeSearchResult { get; set; }

       
    }
}