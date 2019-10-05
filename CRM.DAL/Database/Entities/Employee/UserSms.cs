using System;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Common;

namespace CRM.DAL.Database.Entities.Employee
{
    public class UserSms : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int SmsId { get; set; }
        [ForeignKey("SmsId")]
        public Sms Sms { get; set; }
        public DateTime? SendOn { get; set; }
        public string Comment { get; set; }
    }
}
