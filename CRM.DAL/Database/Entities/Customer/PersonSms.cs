using System;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Common;

namespace CRM.DAL.Database.Entities.Customer
{
    public class PersonSms : BaseEntity
    {
        public int? PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        public int SmsId { get; set; }
        [ForeignKey("SmsId")]
        public Sms Sms { get; set; }
        public DateTime? SendOn { get; set; }
        public string Comment { get; set; }
    }
}
