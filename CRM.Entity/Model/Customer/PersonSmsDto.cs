using System;
using CRM.Entity.Model.Common;

namespace CRM.Entity.Model.Customer
{
    public class PersonSmsDto : BaseDto
    {
        public int? PersonId { get; set; }
        public PersonDto Person { get; set; }
        public int SmsId { get; set; }
        public SmsDto Sms { get; set; }
        public DateTime? SendOn { get; set; }
        public string Comment { get; set; }
    }
}
