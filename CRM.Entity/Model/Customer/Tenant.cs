using System.ComponentModel.DataAnnotations;
using CRM.Entity.Model.Common;

namespace CRM.Entity.Model.Customer
{
    public class Tenant : BaseDto
    {
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
    }
}
