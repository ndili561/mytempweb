using System;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.Entity.Model.Common;

namespace CRM.Entity.Model.Employee
{
    public class UserSmsDto : BaseDto
    {
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public int SmsId { get; set; }
        public SmsDto Sms { get; set; }
        public DateTime? SendOn { get; set; }
        public string Comment { get; set; }
    }
}
