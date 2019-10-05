using System;
using CRM.Dto.Common;

namespace CRM.Dto.Employee
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
