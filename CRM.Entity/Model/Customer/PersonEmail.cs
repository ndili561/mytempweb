using System;
using System.ComponentModel;
using CRM.Entity.Model.Common;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Model.Customer
{
    public class PersonEmailDto : BaseDto
    {
        public int? PersonId { get; set; }
        public PersonDto Person { get; set; }
        public int EmailId { get; set; }
        public EmailDto Email { get; set; }
        public DateTime? ReadOn { get; set; }
        public string Comment { get; set; }
        public int? EmailStatusId { get; set; }
        public EmailStatusDto EmailStatus { get; set; }
        [DefaultValue(false)]
        public bool IsImportant { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
