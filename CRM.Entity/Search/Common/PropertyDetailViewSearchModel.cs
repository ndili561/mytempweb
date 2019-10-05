using System.Collections.Generic;
using System.ComponentModel;
using Core.Utilities;
using CRM.Entity.Model.Common;

namespace CRM.Entity.Search.Common
{
    public class PropertyDetailViewSearchModel : BaseFilterModel
    {
        public PropertyDetailViewSearchModel()
        {
            PropertyDetailViewSearchResult = new List<PropertyDetailViewDto>();
        }
        public List<PropertyDetailViewDto> PropertyDetailViewSearchResult { get; set; }
        [DisplayName("Property Code")]
        public string PropertyCode { get; set; }
        [DisplayName("Property Type")]
        public string PropertyType { get; set; }
        [DisplayName("Number Of Bedrooms")]
        public int? NumberOfBedRooms { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Postcode")]
        public string Postcode { get; set; }
        [DisplayName("Current Main Tenant")]
        public string CurrentMainTenant { get; set; }
    }
}