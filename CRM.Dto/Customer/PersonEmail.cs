using System;
using System.ComponentModel;
using CRM.Dto.Common;

namespace CRM.Dto.Customer
{
    public class PersonEmailDto : BaseDto
    {
        public int? PersonId { get; set; }
        public PersonDto Person { get; set; }
        public int EmailId { get; set; }
        public EmailDto Email { get; set; }
        public DateTime? ReadOn { get; set; }
        public string Comment { get; set; }
        [DefaultValue(false)]
        public bool IsImportant { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
