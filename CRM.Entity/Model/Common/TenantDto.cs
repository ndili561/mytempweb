using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRM.Entity.Model.Customer;

namespace CRM.Entity.Model.Common
{
    public class TenantDto : BaseDto
    {
        public TenantDto()
        {
            TenantHistoryViews = new List<TenantHistoryViewDto>();
        }
        [Display(Name = "Tenant Code")]
        public string TenantCode { get; set; }
        [Display(Name = "Tenancy Reference")]
        public string TenancyReference { get; set; }
        [Display(Name = "Tenancy Type")]
        public string TenancyType { get; set; }
        public int PropertyId { get; set; }
        public PropertyDto Property { get; set; }
        public int PersonId { get; set; }
        public PersonDto Person { get; set; }
        public List<TenantHistoryViewDto> TenantHistoryViews { get; set; }

    }
}
