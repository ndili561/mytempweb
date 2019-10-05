using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Customer;

namespace CRM.Entity.Search.Customer
{
    public class TenantHistoryViewSearchModel : BaseFilterModel
    {
        public TenantHistoryViewSearchModel()
        {
            TenantHistoryViewSearchResult = new List<TenantHistoryViewDto>();
        }
        public List<TenantHistoryViewDto> TenantHistoryViewSearchResult { get; set; }
        public string PropertyCode { get; set; }
        public string TenantCode { get; set; }
        public string TenancyReference { get; set; }
        public string TenancyType { get; set; }
    }
}