using System;

namespace CRM.Entity.Model.Employee
{
    public class MessageDto : BaseDto
    {
        public int? ParentMessageId { get; set; }
        public MessageDto ParentMessage { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool SendEmail { get; set; }
        public bool SendSms { get; set; }
        public DateTime? NextReminderDate { get; set; }
        public int RaisedById { get; set; }
        public virtual UserDto RaisedBy { get; set; }
        public int? UserGroupId { get; set; }
        public virtual UserGroupDto UserGroup { get; set; }
    }
}
