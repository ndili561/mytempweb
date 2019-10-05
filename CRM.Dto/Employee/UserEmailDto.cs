using System;
using System.ComponentModel;
using CRM.Dto.Common;

namespace CRM.Dto.Employee
{
    public class UserEmailDto : BaseDto
    {
        public int UserId { get; set; }
        public UserDto User { get; set; }
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
