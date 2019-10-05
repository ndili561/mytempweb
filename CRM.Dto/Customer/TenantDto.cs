using System;
using System.ComponentModel.DataAnnotations;
using CRM.Dto.Common;

namespace CRM.Dto.Customer
{
    public class TenantDto : BaseDto
    {
        [MaxLength(50)]
        public string TenantCode { get; set; }
        [MaxLength(50)]
        public string TenancyReference { get; set; }
        [MaxLength(50)]
        public string TenancyType { get; set; }
        public int PropertyId { get; set; }
        public string PropertyCode { get; set; }
        public PropertyDto Property { get; set; }
        public int PersonId { get; set; }
        public Guid? PersonKey { get; set; }
        public PersonDto Person { get; set; }
    }
}
