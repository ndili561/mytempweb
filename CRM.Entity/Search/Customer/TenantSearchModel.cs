using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Common;

namespace CRM.Entity.Search.Customer
{
    public class TenantSearchModel : BaseFilterModel
    {
        public TenantSearchModel()
        {
            TenantSearchResult = new List<TenantDto>();
        }
        public List<TenantDto> TenantSearchResult { get; set; }
        public string PropertyCode { get; set; }
        public string TenantCode { get; set; }
        public string TenancyReference { get; set; }
        public string TenancyType { get; set; }
        public int PersonId { get; set; }
    }
}