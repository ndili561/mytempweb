using System;
using System.ComponentModel;
using CRM.Entity.Model.Common;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Model.Employee
{
    public class UserEmailDto : BaseDto
    {
        public int UserId { get; set; }
        public UserDto User { get; set; }
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
